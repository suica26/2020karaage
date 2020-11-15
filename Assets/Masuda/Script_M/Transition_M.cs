using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_M : MonoBehaviour
{
    //中身がよく分からん（勉強不足ごめん）
    Animator animator;
    private float Speed;
    private float Jump, Kick, Blast, Cutter;
    private float pullTime = 0.0f;
    private bool flag = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        Application.targetFrameRate = 60;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Speed = 1.1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Speed = 1.1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Speed = 1.1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Speed = 1.1f;
        }
        else
        {
            Speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump = 2.1f;
        }
        else
        {
            Jump -= 0.02f;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Kick = 2.1f;
        }
        else
        {
            Kick -= 0.1f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            pullTime += 1.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && flag == true && pullTime >= 60)
        {
            StartCoroutine(CreateWave());
            flag = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            pullTime = 0.0f;
        }

        Blast -= 0.1f;

        if (Input.GetKeyDown(KeyCode.J))
        {
            Cutter = 2.1f;
        }
        else
        {
            Cutter -= 0.01f;
        }

        animator.SetFloat("Move", Speed);
        animator.SetFloat("Jump", Jump);
        animator.SetFloat("Kick", Kick);
        animator.SetFloat("Blast", Blast);
        animator.SetFloat("Cutter", Cutter);
    }

    IEnumerator CreateWave()
    {
        for (int i = 0; i < 3; i++)
        {
            Blast = 3.0f;
            yield return new WaitForSeconds(1);
        }
        flag = true;
        yield break;
    }
}
