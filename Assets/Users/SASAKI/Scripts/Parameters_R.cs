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
    [SerializeField] private GameObject hp1, hp2, hp3, hp4;
    [SerializeField] public int score, time, ep, hp, damTime, plusTime, maxHP;
    [SerializeField] public Slider epSlider, hpSlider, hpSlider2, hpSlider3, hpSlider4, mainSlider;
    [SerializeField] public Image circle,niwa;

    private bool freeze = false;
    private float count,niwaPer;
    [SerializeField] public int evo1, evo2, evo3;
    public bool finalChicken;

    public void Start()
    {
        scoreText.text = "Price:$ " + score;
        finalScoreText.text = "Total damage:$ " + score;
        timeText.text = "Time: " + time;
        epText.text = "EP: " + ep;
        hpText.text = "HP: " + hp;
        hpSlider.value = 100;
        hpSlider.maxValue = 100;
        epSlider.value = 0;
        epSlider.maxValue = evo1;
        count = time;
        niwaPer = evo1;
        hp1.SetActive(true);
        hp2.SetActive(false);
        hp3.SetActive(false);
        hp4.SetActive(false);
        mainSlider = hpSlider;
        maxHP = 100;
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
                hp1.SetActive(false);
                hp2.SetActive(true);
                hpSlider2.value = hp;
                mainSlider = hpSlider2;
                TimeManager(10);
                maxHP = 150;
            }
            else if (ep == evo2)
            {
                epSlider.value = 0;
                epSlider.maxValue = evo3 - evo2;
                hp2.SetActive(false);
                hp3.SetActive(true);
                hpSlider3.value = hp;
                mainSlider = hpSlider3;
                TimeManager(10);
                maxHP = 500;
            }
            else if (ep == evo3)
            {
                hp4.SetActive(true);
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

        if (epSlider.value == evo1)
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
        }

        if (hp >= maxHP)
        {
            hp = maxHP;
        }
    }
    //タイマーです。一秒ごとにTimeManager()で一秒減らしてます。
}