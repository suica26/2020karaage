using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commentplaykick : MonoBehaviour
{

    public int mode = 0;
    private Animator animCon;

    public kickID p;

    // Start is called before the first frame update
    void Start()
    {
        animCon = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 1)
        {
            animCon.SetInteger("play", 1);
        }

        if (p.boxscore >= 2)
        {
            animCon.SetInteger("play", 0);
            GetComponent<BoxCollider>().enabled = false;
            mode = 2;
        }

        if (mode == 2)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && p.boxscore != 2)
        {
            mode = 1;
        }
    }
}
