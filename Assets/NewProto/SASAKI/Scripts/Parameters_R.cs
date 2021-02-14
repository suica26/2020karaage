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
    [SerializeField] private Text scoreText, timeText, hpText;
    [SerializeField] private GameObject resultPanel = null;
    [SerializeField] public int score, time, ep, hp, kick, 
        maxEP1, maxEP2, maxEP3, currentEP, getEP;
   
    private bool freeze = false;
    private float count;
    public Slider kickSlider, epSlider, hpSlider;

    void Start()
    {
        scoreText.text = "PRICE : $" + score;
        timeText.text = "Time: " + time;
        hpText.text = "HP: " + hp;
        kickSlider.value = 0;
        epSlider.value = 0;
        epSlider.maxValue = 100;
        hpSlider.value = 50;
        hpSlider.maxValue = 50;
        count = time;
        kick = 1;
    }

    //増田：表示を変えました
    public void ScoreManager(int addScore)      //山本加筆：publicにすることで他Scriptで参照できるようにしました
    {
        if (!freeze)
        {
            score += addScore;
            scoreText.text = "PRICE : $" + score;
            kickSlider.value += kick;
            if (kickSlider.value == 10)
            {
                kickSlider.value = 0;
            }
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
                freeze = true;
                resultPanel.SetActive(true);
            }
        }
    }

    //進化時にタイマーが10秒増える_Mが追加

    //引数で指定した分だけ残りタイムを加算します。

    public void EPManager(int addEP)
    {
        if (!freeze)
        {
            ep += addEP;
            TimeManager(+1);
            currentEP = currentEP + getEP;
            epSlider.value = currentEP;
            if (ep == 100)
            {
                TimeManager(+10);
            }
            else if (ep == 250)
            {
                TimeManager(+10);
            }
            else if (ep == 500)
            {
                TimeManager(+10);
            }
        }
    }
    //引数で指定した分だけEPを加算します。

    public void HPManager(int addHP)
    {
        if (!freeze)
        {
            hp -= addHP;
            hpSlider.value -= addHP;
           /*if (hp <= 0)
            *{
            *    freeze = true;
            *    resultPanel.SetActive(true);
            *    hp = 0;
            *}
            *hpText.text = "HP: " + hp;
            */
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

        if (epSlider.value == maxEP1)
        {
            epSlider.value = 0;
            currentEP = 0;
            epSlider.maxValue = 150;
            hpSlider.maxValue = 80;
            hpSlider.value += 30; 
            maxEP1 = 10000;
        }
        else if (epSlider.value == maxEP2)
        {
            epSlider.value = 0;
            currentEP = 0;
            epSlider.maxValue = 250;
            hpSlider.maxValue = 160;
            hpSlider.value += 80;
            maxEP2 = 10000;
        }else if(epSlider.value == maxEP3)
        {
            hpSlider.value += 340;
        }
    }
    //タイマーです。一秒ごとにTimeManager()で一秒減らしてます。
}