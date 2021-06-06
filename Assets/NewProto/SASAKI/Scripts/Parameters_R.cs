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

    [SerializeField] public int score, time, ep, hp, maxEP1, maxEP2, maxEP3, maxHP1, maxHP2, maxHP3, plusHP1, plusHP2, plusHP3, sp, maxSP, damTime, plusTime;
    //[SerializeField] public Slider hpSlider, epSlider, kickSlider;
    [SerializeField] public Image circle,niwa;

    private bool freeze = false;
    private float count,niwaPer;
    [SerializeField] private int evo1, evo2, evo3;

    public void Start()
    {
        scoreText.text = "Price:$ " + score;
        finalScoreText.text = "Total damage:$ " + score;
        timeText.text = "Time: " + time;
        epText.text = "EP: " + ep;
        hpText.text = "HP: " + hp;
        /*hpSlider.value = 100;
        hpSlider.maxValue = 100;
        epSlider.value = 0;
        epSlider.maxValue = 30;
        kickSlider.value = 0;
        kickSlider.maxValue = maxSP;*/
        count = time;
        niwaPer = evo1;
    }

    public void ScoreManager(int addScore)      //山本加筆：publicにすることで他Scriptで参照できるようにしました
    {
        if (!freeze)
        {
            score += addScore;
            scoreText.text = "Price:$ " + score;
            //kickSlider.value += 1;
  
            /*if (Input.GetMouseButton(0))
            {
                if (kickSlider.value == sp)
                {
                    kickSlider.value = 0;
                }
            }*/
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
            niwa.fillAmount += 5 / niwaPer;
            //epSlider.value += addEP;
            epText.text = "EP: " + ep;
            if (ep == evo1)
            {
                niwa.fillAmount = 0;
                niwaPer = evo2;
                TimeManager(10);
            }
            else if (ep == evo2)
            {
                niwa.fillAmount = 0;
                niwaPer = evo3;
                TimeManager(10);
            }
            else if (ep == evo3)
            {
                TimeManager(10);
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
            circle.fillAmount -= addHP / 10;
            //hpSlider.value -= addHP;
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

        /*if (epSlider.value == maxEP1)
        {
            epSlider.value = 0;
            epSlider.maxValue = maxEP2;
            maxEP1 = 10000;
            /*hpSlider.maxValue = maxHP1;
            hpSlider.value += plusHP1;
            hp += plusHP1; 
        }*/
        /*else if (epSlider.value == maxEP2)
        {
            epSlider.value = 0;
            epSlider.maxValue = maxEP3;
            maxEP2 = 10000;
            /*hpSlider.maxValue = maxHP2;
            hpSlider.value += plusHP2;
            hp += plusHP2;
            
        }*/
        /*else if (epSlider.value == maxEP3)
        {
            hpSlider.maxValue = maxHP3;
            hpSlider.value += plusHP3;
            hp += plusHP3;
        }
        */
    }
    //タイマーです。一秒ごとにTimeManager()で一秒減らしてます。
}