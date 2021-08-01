using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Y : ObjectStateManagement_Y
{
    public float KnockBackPower;    //ノックバックする量
    private Animator animator;
    private NavMeshAgent agent;
    private Rigidbody rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    //挙動を記述
    void Update()
    {
    }

    protected void KnockBack()
    {
        agent.enabled = false;
        rb.isKinematic = true;

        animator.SetTrigger("KnockBack");

        rb.isKinematic = false;
        agent.enabled = true;
    }

    protected override void Death()
    {
        //すでに破壊済みの場合は無視
        if (!ChangeToDeath()) return;

        //アニメーション以外の要素を停止
        agent.enabled = false;
        rb.isKinematic = true;

        animator.SetTrigger("Death");
    }
}
