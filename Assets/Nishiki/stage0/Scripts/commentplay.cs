using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commentplay : MonoBehaviour
{

    public int mode = 0;
    private Animator animCon;

    public doorscore option;

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

        if (option.nowanim == 5)
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
        if (collision.gameObject.tag == "Player" && option.nowanim != 5 || collision.gameObject.tag == "Player" && option.nowanim != 6)
        {

            mode = 1;

        }
    }
}
