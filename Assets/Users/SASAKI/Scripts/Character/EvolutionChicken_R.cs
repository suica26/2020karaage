﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionChicken_R : MonoBehaviour
{
    [SerializeField] private GameObject objParam;
    [SerializeField] private GameObject[] chickens;

    [SerializeField] private int[] evolutionPoint;

    [SerializeField] private float[] HP;
    [SerializeField] private float[] ATK;
    [SerializeField] private float[] SPD;
    [SerializeField] private float[] SCORE;
    [SerializeField] private float[] JUMP;

    [SerializeField] private bool DEBUG_MODE;

    private Parameters_R scrParam;
    private MorBlast_R scrBlast;
    private int EP;

    //ステータス設定用変数
    private int evolutionNum;
    private float status_HP;
    private float status_ATK;
    private float status_SPD;
    private float status_SCORE;
    private float status_JUMP;

    //カプセル化
    public int EvolutionNum { get { return evolutionNum; } }
    public float Status_HP { get { return status_HP; } }
    public float Status_ATK { get { return status_ATK; } }
    public float Status_SPD { get { return status_SPD; } }
    public float Status_SCORE { get { return status_SCORE; } }
    public float Status_JUMP { get { return status_JUMP; } }

    private CriAtomSource audioLavel;
    private CriAtomSource BGM;

    void Start()
    {
        scrParam = objParam.gameObject.GetComponent<Parameters_R>();
        scrBlast = GetComponent<MorBlast_R>();

        evolutionNum = 0;
        status_HP = HP[evolutionNum];
        status_ATK = ATK[evolutionNum];
        status_SPD = SPD[evolutionNum];
        status_SCORE = SCORE[evolutionNum];
        status_JUMP = JUMP[evolutionNum];

        chickens[1].SetActive(false);
        chickens[2].SetActive(false);
        chickens[3].SetActive(false);

        audioLavel = (CriAtomSource)GetComponent("CriAtomSource");
        //ADX Selector Change
        audioLavel.player.SetSelectorLabel("Chicken_Form", "form1");

    }

    // Update is called once per frame
    void Update()
    {
        if (DEBUG_MODE)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                scrParam.EPManager(50);
            }
        }
        EP = scrParam.ep;

        if(evolutionNum < evolutionPoint.Length && EP >= evolutionPoint[evolutionNum])
        {
            evolutionNum++;

            chickens[evolutionNum - 1].SetActive(false);
            chickens[evolutionNum].SetActive(true);

            //ADX Selector Change
            audioLavel.player.SetSelectorLabel("Chicken_Form", "form2");
            audioLavel.Play("ShockWave");

            status_HP = HP[evolutionNum];
            status_ATK = ATK[evolutionNum];
            status_SPD = SPD[evolutionNum];
            status_SCORE = SCORE[evolutionNum];
            status_JUMP = JUMP[evolutionNum];

            scrBlast.EvoBlast();
        }
    }
}