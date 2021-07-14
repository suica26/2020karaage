using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commentplaykick : MonoBehaviour
{
    private Animator animCon;
    private int breakNum;

    // Start is called before the first frame update
    void Start()
    {
        animCon = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && breakNum != 2)
        {
            animCon.SetInteger("play", 1);
        }
    }

    public void BreakNumPlus()
    {
        breakNum++;
        if (breakNum >= 2)
        {
            animCon.SetInteger("play", 0);
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
