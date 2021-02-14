using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_R : MonoBehaviour
{
    Animator animator;
    private float Speed;
    private float Jump, Kick, Blast, Cutter, FACutter, FAKick;
    private float timeBlast, timeCutter, timeKick;
    private bool flag = true;

    void Start()
    {
        timeBlast = 0.0f;
        timeCutter = 0.0f;
        timeKick = 0.0f;
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

        if (Input.GetMouseButton(0))
        {
            timeKick += Time.deltaTime;
            if (timeKick >= 0.5f)
            {
                FAKick = 5.0f;
                timeKick = 0.0f;
            }
        }
        else
        {
            timeKick = 0.0f;
        }
        FAKick -= 0.1f;

        if (Input.GetMouseButtonDown(0))
        {
            Kick = 2.1f;
        }
        else
        {
            Kick -= 0.1f;
        }

        if (Input.GetMouseButton(2))
        {
            timeBlast += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(2) && flag == true && timeBlast >= 1.0f)
        {
            StartCoroutine(CreateWave());
            flag = false;
        }
        if(Input.GetMouseButtonUp(2))
        {
            timeBlast = 0.0f;
        }

        Blast -= 0.1f;
        if (Input.GetMouseButton(1))
        {
            timeCutter += Time.deltaTime;
            if(timeCutter >= 0.5f)
            {
                FACutter = 5.0f;
                timeCutter = 0.0f;
            }
        }
        else if(Input.GetMouseButtonUp(1))
        {
            timeCutter = 0.0f;
        }
        FACutter -= 0.1f;

        if (Input.GetMouseButtonDown(1))
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
        animator.SetFloat("FallAttackCutter", FACutter);
        animator.SetFloat("FallAttackKick", FAKick);
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
