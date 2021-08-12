using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Y : ObjectStateManagement_Y
{
    [Header("ここから敵の設定")]
    protected Animator animator;
    protected NavMeshAgent agent;
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
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
    }

    //基本挙動を記述
    protected void Update()
    {
        routineTimer += Time.deltaTime;
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance && routineTimer > attackInterval)
        {
            routineTimer = 0f;
            Attack();
        }
    }

    protected void StopMove()
    {
        agent.enabled = false;
        rb.isKinematic = true;
    }

    protected IEnumerator RestartMove()
    {
        yield return new WaitForSeconds(attackClip.length);
        rb.isKinematic = false;
        agent.enabled = true;
    }

    protected virtual void Attack()
    {
        StopMove();
        animator.SetTrigger("Attack");
    }

    protected override void Death()
    {
        //すでに破壊済みの場合は無視
        if (!ChangeToDeath()) return;

        //アニメーション以外の要素を停止
        StopMove();

        animator.SetTrigger("Death");
    }
}
