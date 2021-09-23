using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commentplaykick : MonoBehaviour
{
    public Animator animCon;
    private int breakNum;

    int time;
    public int ontime;
    bool a = false;

    // Start is called before the first frame update
    void Start()
    {
        animCon = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (a == false)
        {
            time++;
        }

        if (time >= ontime && breakNum != 1)
        {
            animCon.SetInteger("play", 1);
            a = true;

            time = 0;
        }
    }

    public void BreakNumPlus()
    {
        breakNum++;
        if (breakNum >= 1)
        {
            animCon.SetInteger("play", 0);
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
