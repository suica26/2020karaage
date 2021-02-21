using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Y : MonoBehaviour
{
    [Range(0, 4), SerializeField] private int tier_WalkAttack;
    [Range(0, 4), SerializeField] private int tier_ChargeKick;
    public int HP = 0;
    [Header("ダメージ倍率")]
    public float kickMag;             //キックのダメージ倍率
    public float blastMag;         //ブラストのダメージ倍率
    public float cutterMag;        //カッターのダメージ倍率
    public float chargeKickMag;       //ためキックのダメージ倍率
    public float fallAttackMag;     //落下攻撃のダメージ倍率
    public float KnockBackPower;    //ノックバックする量
    [Header("破壊時のスコア")] public int breakScore;                          //建物を破壊したときに得られるスコア
    [Header("破壊時のチャージポイント")] public int breakPoint;                //建物を破壊したときに得られるチャージポイント
    private Animator animator;
    private GameObject player;
    private FoodMaker_R scrFood;
    private chickenKick_R scrKick;
    private EvolutionChicken_R scrEvo;
    private Rigidbody rb;
    private bool live = true;
    private bool damage = false;
    private int hitSkilID = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        scrKick = player.GetComponent<chickenKick_R>();
        scrEvo = player.GetComponent<EvolutionChicken_R>();
        scrFood = GetComponent<FoodMaker_R>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0 && live)
        {
            GameObject.Find("Canvas").GetComponent<Parameters_R>().ScoreManager(breakScore);
            if (scrFood != null)
            {
                scrFood.DropFood();
            }
            Death();
        }
        else
        {
            if (rb.velocity.magnitude < 5f)
            {
                animator.applyRootMotion = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //キックダメージ
        if (other.gameObject.name == "KickCollision")
        {
            if (scrKick.chargePoint >= 100)
            {
                if (scrEvo.EvolutionNum >= tier_ChargeKick)
                {
                    HP = 0;
                    scrKick.chargePoint = 0;
                    hitSkilID = 4;
                }
                else
                {
                    HP -= (int)(scrEvo.Status_ATK * chargeKickMag);
                }
            }
            else
            {
                HP -= (int)(scrEvo.Status_ATK * kickMag);
                hitSkilID = 1;
            }
            damage = true;
        }
        //ブラストダメージ
        if (other.gameObject.name == "MorningBlastSphere_Y(Clone)")
        {
            HP -= (int)(scrEvo.Status_ATK * blastMag / 3f);
            hitSkilID = 2;
            damage = true;
        }
        //カッターダメージ
        if (other.gameObject.name == "Cutter(Clone)")
        {
            HP -= (int)(scrEvo.Status_ATK * cutterMag);
            hitSkilID = 3;
            damage = true;
        }
        //連鎖破壊の破片
        if (other.gameObject.tag == "Chain")
        {
            var chainScript = other.gameObject.GetComponent<ChainBreak_Y>();
            HP -= chainScript.chainDamage;
            hitSkilID = 0;
            damage = true;
        }
        //落下攻撃の衝撃波
        if (other.gameObject.name == "fallAttackCircle(Clone)")
        {
            HP -= (int)(scrEvo.Status_ATK * fallAttackMag);
            KnockBack();
            hitSkilID = 1;
            damage = true;
        }

        if (damage)
        {
            //振動させる
            StartCoroutine(DoShake(0.25f, 0.1f));
            damage = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //踏みつぶし攻撃
        if (collision.gameObject.tag == "Player" && scrEvo.EvolutionNum >= tier_WalkAttack)
        {
            HP = 0;
            hitSkilID = 5;
        }
    }

    private void KnockBack()
    {
        animator.applyRootMotion = false;
        rb.AddForce(-transform.forward * KnockBackPower, ForceMode.Impulse);
    }

    //振動コルーチン
    private IEnumerator DoShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = pos;
    }

    private void Death()
    {
        GetComponent<EnemyNav_Y>().live = false;
        Destroy(GetComponent<EnemyNav_Y>());
        GetComponent<NavMeshAgent>().enabled = false;
        Destroy(this.gameObject, 3f);
        if (GetComponent<CapsuleCollider>() != null) Destroy(GetComponent<CapsuleCollider>());
        live = false;
        animator.SetBool("isDeath", true);
        if (hitSkilID == 3 || hitSkilID == 8)
        {
            GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.freezeRotation = false;
            rb.useGravity = true;
            var F = transform.position - player.transform.position;
            F.y = 0;
            F = F.normalized;
            F.y = 0.6f;
            animator.applyRootMotion = false;
            rb.AddForce(F * 50f, ForceMode.Impulse);
            float torque = 5f;
            Vector3 TorquePower = new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque));
            rb.AddTorque(TorquePower, ForceMode.Impulse);
        }
    }
}
