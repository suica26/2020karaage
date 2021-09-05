using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 * -------------------------
 * 
 * ||Parameters_R()
 * ||
 * || =ScoreManager(int)
 * ||  =引数で指定した分スコアを加算します。
 * ||
 * || =TimeManager(int)
 * ||  =引数で指定した分タイムを加算します。
 * ||
 * || =EPManager(int)
 * ||  =引数で指定した分EPを加算します。
 * ||
 * || =HPManager(int)
 * ||  =引数で指定した分HPを加算します。
 *
 * -------------------------
 */

public class Parameters_R : MonoBehaviour
{
    [SerializeField] private Text scoreText, finalScoreText, timeText, epText, hpText;
    [SerializeField] private GameObject resultPanel = null;
    [SerializeField] private GameObject[] hpSli;
    [SerializeField] public int score, time, ep, hp, damTime, plusTime, maxHP, niwaPer;
    [SerializeField] public Slider epSlider, mainSlider;
    [SerializeField] public Slider[] hpSlider;

    private bool freeze = false;
    private float count;
    [SerializeField] public int evo1, evo2, evo3, startNum;
    public string saveStage;
    [SerializeField] public Missions_M scrMis;

    public void Start()
    {
        scoreText.text = "Price:$ " + score;
        finalScoreText.text = "Total damage:$ " + score;
        timeText.text = "Time: " + time;
        epText.text = "EP: " + ep;
        hpText.text = "HP: " + hp;
        epSlider.value = 0;
        //epSlider.maxValue = evo1;
        count = time;
        for (int i = 0; i < 4; i++)
        {
            if (i == startNum)
            {
                hpSli[i].SetActive(true);
                mainSlider = hpSlider[i];
            }
            else
            {
                hpSli[i].SetActive(false);
            }
        }
        ep += niwaPer;
        EPManager(0);
    }

    public void ScoreManager(int addScore)      //山本加筆：publicにすることで他Scriptで参照できるようにしました
    {
        if (!freeze)
        {
            score += addScore;
            scoreText.text = "Price:$ " + score;
        
        }
    }
    //引数で指定した分だけスコアを加算します。

    public void TimeManager(int addTime)
    {
        if (!freeze)
        {
            time += addTime;
            count = time;
            timeText.text = "Time: " + time;
            if (time <= 0)
            {
                finalScoreText.text = "Total damage:$ " + score;
                freeze = true;
                resultPanel.SetActive(true);
            }
        }
    }
    //引数で指定した分だけ残りタイムを加算します。

    public void EPManager(int addEP)
    {
        if (!freeze)
        {
            ep += addEP;
            plusTime += 1;
            epSlider.value += addEP;
            hp += 1;
            mainSlider.value += 1;
            //epText.text = "EP: " + ep;
            if (ep == evo1)
            {
                epSlider.value = 0;
                epSlider.maxValue = evo2 - evo1;
                hpSli[0].SetActive(false);
                hpSli[1].SetActive(true);
                hpSlider[1].value = hp;
                mainSlider = hpSlider[1];
                TimeManager(10);
                maxHP = 150;
            }
            else if (ep == evo2)
            {
                epSlider.value = 0;
                epSlider.maxValue = evo3 - evo2;
                hpSli[1].SetActive(false);
                hpSli[2].SetActive(true);
                hpSlider[2].value = hp;
                mainSlider = hpSlider[2];
                TimeManager(10);
                maxHP = 500;
            }
            else if (ep == evo3)
            {
                hpSli[3].SetActive(true);
                TimeManager(10);
                maxHP = 1000;
            }

            if (plusTime == 10)
            {
                plusTime = 0;
                TimeManager(1);
            }
            
        }
    }
    //引数で指定した分だけEPを加算します。

    public void HPManager(int addHP)
    {   
        if (!freeze)
        {
            TimeManager(-damTime);
            hp -= addHP;
            mainSlider.value -= addHP;

            if (hp <= 0)
            {
                freeze = true;
                resultPanel.SetActive(true);
                hp = 0;
                PlayerPrefs.SetString(saveStage, scrMis.load);
                PlayerPrefs.Save();
            }
            hpText.text = "HP: " + hp;
        }
    }
    //引数で指定した分だけHPを加算します。

    private void Update()
    {
        count -= Time.deltaTime;
        
        if (time - count > 1)
        {
            TimeManager(-1);
        }

        /*if (epSlider.value == evo1)
        {
            epSlider.value = 0;
            epSlider.maxValue = evo2;
            evo1 = 100000;
        }

        else if (epSlider.value == evo2)
        {
            epSlider.value = 0;
            epSlider.maxValue = evo3;
            evo2 = 100000;
        }

        else if (epSlider.value == evo3)
        {
            hp4.SetActive(true);
        }*/

        if (hp >= maxHP)
        {
            hp = maxHP;
        }

        if (hp >= 500)
        {
            mainSlider = hpSlider[3];
        }
        else if (hp < 500)
        {
            mainSlider = hpSlider[2];
        }
    }
    //タイマーです。一秒ごとにTimeManager()で一秒減らしてます。

}