using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Y : ObjectStateManagement_Y
{
    [Header("ここから敵の設定")]
    protected Animator animator;
    protected NavMeshAgent navAgent;
    protected Rigidbody rb;
    //攻撃を行うプレイヤーとの距離
    public float attackDistance;
    protected float routineTimer;
    public float attackInterval;
    public GameObject weapon;
    public AnimationClip attackClip;
    public int attackDamage;
    public float searchArea = 50f;
    private EnemyMoveController_Y enemyControllerScr;

    private ADX_BGMAISAC aisacScr;
    [SerializeField] private Vector3[] patrollRoute;
    public EnemySpawnController AIscript;
    public int number;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
        animator.SetBool("isWalk", true);
        enemyControllerScr = GameObject.Find("GameAI_Y").GetComponent<EnemyMoveController_Y>();
        aisacScr = GameObject.Find("BGMObject").GetComponent<ADX_BGMAISAC>();
    }

    //基本挙動を記述
    protected virtual void Update()
    {
        if (livingFlg)
        {
            if (enemyControllerScr.enemyCanMove)
            {
                routineTimer += Time.deltaTime;

                if (Vector3.Distance(player.transform.position, transform.position) <= searchArea)
                {
                    aisacScr.SetBattleBGM(gameObject);

                    if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
                    {
                        if (routineTimer > attackInterval)
                        {
                            routineTimer = 0f;
                            Attack();
                        }
                        else
                        {
                            Wait();
                        }
                    }
                    else
                    {
                        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("StopMove"))
                        {
                            navAgent.SetDestination(player.transform.position);
                            Walk();
                        }
                    }
                }
                else
                {
                    Patroll();
                }
            }
            else StopMove();
        }
    }

    protected void StopMove()
    {
        navAgent.isStopped = true;
        rb.isKinematic = true;
    }

    protected void Move()
    {
        navAgent.isStopped = false;
        rb.isKinematic = false;
    }

    protected IEnumerator RestartMove()
    {
        yield return new WaitForSeconds(attackClip.length);
        Move();
    }

    protected void Wait()
    {
        StopMove();
        var dir = player.transform.position - transform.position;
        dir.y = 0f;
        transform.forward = dir.normalized;
        animator.SetBool("isWait", true);
        animator.SetBool("isWalk", false);
    }

    protected void Walk()
    {
        Move();
        animator.SetBool("isWait", false);
        animator.SetBool("isWalk", true);
    }

    protected void Patroll()
    {
        if (patrollRoute == null)
        {
            Wait();
        }
        else
        {
            Walk();
            if (navAgent.remainingDistance <= 3.0f)
            {
                navAgent.SetDestination(patrollRoute[Random.Range(0, patrollRoute.Length)]);
            }
        }
    }

    protected virtual void Attack()
    {
        StopMove();
        animator.SetTrigger("Attack");
        StartCoroutine(RestartMove());
    }

    public override void Damage(float mag, int skill)
    {
        //すでに破壊済みの場合は何も起きないようにする
        if (!livingFlg) return;

        HP -= (int)(scrEvo.Status_ATK * mag);
        SetSkillID(skill);
        animator.SetTrigger("Damage");

        //生死判定
        LivingCheck();
    }

    protected override void Death()
    {
        //すでに破壊済みの場合は無視
        if (!ChangeToDeath()) return;

        //アニメーション以外の要素を停止
        StopMove();
        navAgent.enabled = false;

        Destroy(gameObject, 1.5f);
        animator.SetTrigger("Death");

        //ステージ3用の処理
        var st3BoosScr = Stage3BossBuilding_Y.ReturnInstance();
        if (st3BoosScr != null) st3BoosScr.IncreaseEnemyBreakCount();
    }

    public void SetPatrollRoute(Vector3[] route)
    {
        patrollRoute = route;
    }

    protected void OnDestroy()
    {
        AIscript?.UpdateEnemyNum(number);
    }
}
