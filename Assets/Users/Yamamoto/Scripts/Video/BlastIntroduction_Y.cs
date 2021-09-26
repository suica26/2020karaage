﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BlastIntroduction_Y : MonoBehaviour
{
    public GameObject topObject, mission, subMission, exMission;
    private Text mis, subMis, exMis;
    public Animation misAnim;
    public float introStart;
    public VideoPlayer movie;
    public bool isIntro, isBlast, blasted;
    public float blastCharge;
    public double videoTime;
    private SaveManager_Y saveManager;
    public bool isJapanese;
    private CriAtomSource Sound;
    private string[] introText = new string[6]
    {
        "おはようブラストを使ってみよう！",
        "おはようブラストをチャージ！",
        "マウスホイールを長押ししよう！",
        "Let's use Morning Blast!",
        "Charge Morning Blast!",
        "Press Mouse wheel!",
    };
    private string[] blastText = new string[6]
    {
        "おはようブラストを使ってみよう！",
        "おはようブラストを発射！",
        "マウスホイールを離そう！",
        "Let's use Morning Blast!",
        "Play Morning Blast!",
        "Release Mouse wheel!"
    };
    private string[] mobileText = new string[4]
    {
        "ブラストボタンを長押しよう！",
        "ブラストボタンを離そう！",
        "Press Blast Button!",
        "Release Mouse Button!"
    };

    public bool isMoblie;

    // Start is called before the first frame update
    void Start()
    {
        mis = mission.GetComponent<Text>();
        subMis = subMission.GetComponent<Text>();
        exMis = exMission.GetComponent<Text>();
        blastCharge = 0f;
        isIntro = isBlast = blasted = false;

        var saveObj = GameObject.Find("SaveManager");
        if (saveObj != null)
            saveManager = saveObj.GetComponent<SaveManager_Y>();
        if (saveManager == null || saveManager.GetLanguage() == "Japanese") isJapanese = true;
        else if (saveManager.GetLanguage() == "English") isJapanese = false;

        Sound = GetComponent<CriAtomSource>();

        if (isMoblie)
        {
            introText[2] = mobileText[0];
            introText[5] = mobileText[1];
            blastText[2] = mobileText[2];
            blastText[5] = mobileText[3];
        }

#if UNITY_IOS || UNITY_ANDROID
        isMoblie = true;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        videoTime = movie.time;
        bool pressBlast = Input.GetMouseButtonDown(2);
        bool upBlast = Input.GetMouseButtonUp(2);

        if (!blasted)
        {
            if (movie.time >= introStart && !isIntro) Intro_Start();
            if (isIntro && !isBlast)
            {
                if (pressBlast)
                {
                    Sound.Play("MovieBlast1");
                    blastCharge += Time.deltaTime;
                }
                else
                {
                    Sound.Stop();
                    blastCharge = 0f;
                }
                if (blastCharge >= 3f) Intro_LetsBlast();

            }
            if (isBlast && upBlast)
            {
                movie.Play();
                blasted = true;
                topObject.SetActive(false);
                Sound.Play("MovieBlast2");
            }
        }
    }

    private void Intro_Start()
    {
        topObject.SetActive(true);
        isIntro = true;
        movie.Pause();
        misAnim.Play();

        var num = 0;
        if (isJapanese) num = 0;
        else num = 3;
        mis.text = introText[num];
        subMis.text = introText[num + 1];
        exMis.text = introText[num + 2];
    }

    private void Intro_LetsBlast()
    {
        isBlast = true;
        misAnim.Play();

        var num = 0;
        if (isJapanese) num = 0;
        else num = 3;
        mis.text = blastText[num];
        subMis.text = blastText[num + 1];
        exMis.text = blastText[num + 2];
    }
}
