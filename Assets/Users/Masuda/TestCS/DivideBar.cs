using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivideBar : MonoBehaviour
{
    public int[] hps;
    public int skip, maxHP, hp;
    public float nowHP, percent, atai;
    public bool judge1, judge2;
    public Slider[] hpSli;
    public Slider mainSli;


    void Start()
    {
        judge1 = false;
        judge2 = false;
        nowHP = maxHP;
        mainSli = hpSli[1];
        for (int i = 0; i < 4; i++)
        {
            hps[i] = 340;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            hp -= 25;
            nowHP -= 25;
            mainSli.value -= 25;
            for (int i = 0; i < 4; i++)
            {
                hps[i] -= 25;
            }
            if (hps[0] >= 60 && hps[0] <= 84)
            {
                judge1 = true;
            }
            if (hps[1] >= 120 && hps[1] <= 144)
            {
                judge1 = true;
            }
            if (hps[2] >= 230 && hps[2] <= 254)
            {
                judge1 = true;
            }
            if (hps[3] >= 290 && hps[3] <= 314)
            {
                judge1 = true;
            }
            if (hpSli[1].value == 0)
            {
                mainSli = hpSli[0];
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            hp -= 50;
            nowHP -= 50;
            for (int i = 0; i < 4; i++)
            {
                hps[i] -= 50;
            }
            if (hps[0] >= 60 && hps[0] <= 110)
            {
                judge1 = true;
            }
            if (hps[1] >= 120 && hps[1] <= 170)
            {
                judge1 = true;
            }
            if (hps[2] >= 230 && hps[2] <= 280)
            {
                judge1 = true;
            }
            if (hps[3] >= 290 && hps[3] <= 315)
            {
                judge1 = true;
            }
            if (hpSli[1].value == 0)
            {
                mainSli = hpSli[0];
            }
            if (hpSli[1].value == 25)
            {
                mainSli.value -= 25;
                mainSli = hpSli[0];
                mainSli.value -= 25;
            }
            else
            {
                mainSli.value -= 50;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (hp == 50 || hp == 110 || hp == 220 || hp == 280)
            {
                judge2 = true;
            }
            hp += 1;
            nowHP += 1;
            mainSli.value += 1;
            for (int i = 0; i < 4; i++)
            {
                hps[i] += 1;
            }
            if (hpSli[0].value == 170)
            {
                mainSli = hpSli[1];
            }
        }

        if (judge1)
        {
            hp -= 10;           
            mainSli.value -= 10;
            judge1 = false;
        }
        if (judge2)
        {
            hp += 10;
            mainSli.value += 10;
            judge2 = false;
        }
    }
}