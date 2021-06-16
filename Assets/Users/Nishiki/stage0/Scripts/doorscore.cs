using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscore : MonoBehaviour
{

    private Animator animCon;

    //public AudioClip breakdoor;
    private AudioSource Sound;

    public kickID chicken;

    public int damage;

    public int breakpoint01;
    public int breakpoint02;
    public int breakpoint03;
    public int breakpoint04;
    public int breakpoint05;

    public int nowanim;

    public GameObject air;
    public GameObject arrow;

    //ADX
    public new CriAtomSource audio;
    bool isOnce = true;

    int time;

    // Start is called before the first frame update
    void Start()
    {
        animCon = GetComponent<Animator>();
        Sound = GetComponent<AudioSource>();
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    // Update is called once per frame
    void Update()
    {

        if (damage >= breakpoint01)
        {

            animCon.SetInteger("bp", 1);
            nowanim = 1;

        }

        if (damage >= breakpoint02)
        {

            animCon.SetInteger("bp", 2);
            nowanim = 2;

        }

        if (damage >= breakpoint03)
        {

            animCon.SetInteger("bp", 3);
            nowanim = 3;

        }

        if (damage >= breakpoint04)
        {

            animCon.SetInteger("bp", 4);
            nowanim = 4;

        }

        if (damage >= breakpoint05)
        {

            animCon.SetInteger("bp", 5);
            nowanim = 5;

        }

        if (nowanim == 5)
        {

            time = time + 1;

        }

        if (time >= 50)
        {

            air.SetActive(false);
            arrow.SetActive(true);

            nowanim = 6;
            time = 0;

            if (isOnce)
            {
                audio.Play("door_broken00");
                isOnce = false;
            }
            

        }


    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (chicken.id == 1)
            {

                damage = damage + 1;
                audio.Play("Track_noise00");

            }


        }
    }


}
