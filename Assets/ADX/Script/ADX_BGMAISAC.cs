﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ADX_BGMAISAC : MonoBehaviour
{
    private float BGMAISAC;
    public CriAtomSource bgmCriAtomSource;//BGMのCriAtomSourceアタッチしないと効かない
    EvolutionChicken_R scrEvo;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        BGMAISAC = 0f;
        bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);

        if (SceneManager.GetActiveScene().name == "stage1")
        {
            bgmCriAtomSource.Play("BGM01");
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            bgmCriAtomSource.Play("BGM01");
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            bgmCriAtomSource.Play("BGM03");
        }
        else if (SceneManager.GetActiveScene().name == "stage0")
        {
            bgmCriAtomSource.Play("BGM_St0");
        }
        else bgmCriAtomSource.Play("BGM00");

        player = GameObject.Find("Player");
        scrEvo = player.GetComponent<EvolutionChicken_R>();
    }

    // Update is called once per frame
    void Update()
    {
        //nullチェックを追加　山本
        if (scrEvo != null)
        {
            if (scrEvo.EvolutionNum == 1 && BGMAISAC < 1 && SceneManager.GetActiveScene().name == "stage1")
            {
                BGMAISAC += 0.02f;
                bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);
            }
            else if (scrEvo.EvolutionNum == 1 && SceneManager.GetActiveScene().name == "Stage2")
            {
                BGMAISAC = 0f;
            }
            else if (scrEvo.EvolutionNum == 2 && BGMAISAC < 1 && SceneManager.GetActiveScene().name == "Stage2")
            {
                BGMAISAC += 0.02f;
                bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);
            }
        }
    }
}
