using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boxhp : MonoBehaviour
{
    public int damage;
    public int maxHp = 155;
    int currentHp;
    public Slider slider;
    public GameObject uipanel;
    public kickID p;
    //ADX
    /*
    public new CriAtomSource audio;
    bool isOnce = true;
    private AudioSource Sound;
    */
    private Animator animCon;
    int mode = 0;
    int time;
    int breaktime;

    // Start is called before the first frame update
    void Start()
    {
        animCon = GetComponent<Animator>();
        /*
        Sound = GetComponent<AudioSource>();
        audio = (CriAtomSource)GetComponent("CriAtomSource");
        */

        slider.value = 1;
        currentHp = maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)currentHp / (float)maxHp;

        if (currentHp <= 0)
        {
            breaktime++;
            animCon.SetInteger("break", 1);
            Destroy(this.gameObject, 1.2f);
        }

        if (breaktime == 10)
        {
            p.boxscore = p.boxscore + 1;
        }

        if (mode == 1)
        {
            uipanel.SetActive(true);
            time++;
        }

        if (time >= 70)
        {
            mode = 0;
        }

        if (mode == 0)
        {
            uipanel.SetActive(false);
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mode = 1;

            if (p.id == 1)
            {
                time = 0;
                currentHp = currentHp - damage;
                //audio.Play("Track_noise00");
            }
        }
        else
        {
            mode = 0;
        }
    }
}
