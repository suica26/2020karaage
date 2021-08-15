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

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
        animator.SetBool("isWalk", true);
    }

    //基本挙動を記述
    protected void Update()
    {
        routineTimer += Time.deltaTime;

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
            Walk();
        }
    }

    protected void StopMove()
    {
        navAgent.isStopped = true;
        rb.isKinematic = true;
    }

    protected void Move()
    {
        navAgent.destination = player.transform.position;
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
        animator.SetBool("isWait", true);
        animator.SetBool("isWalk", false);
    }

    protected void Walk()
    {
        Move();
        animator.SetBool("isWait", false);
        animator.SetBool("isWalk", true);
    }

    protected void Attack()
    {
        StopMove();
        animator.SetTrigger("Attack");
        StartCoroutine(RestartMove());
    }

    protected override void Death()
    {
        //すでに破壊済みの場合は無視
        if (!ChangeToDeath()) return;

        //アニメーション以外の要素を停止
        StopMove();

        Destroy(gameObject, 0.5f);
        animator.SetTrigger("Death");
    }
}
