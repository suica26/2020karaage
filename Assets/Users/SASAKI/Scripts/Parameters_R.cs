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
    [SerializeField] private Text scoreText, finalScoreText, timeText;
    [SerializeField] private GameObject resultPanel = null;
    [SerializeField] private GameObject[] hpSli;
    [SerializeField] public int ep, hp, maxHP, niwaPer;
    [SerializeField] public Slider epSlider, mainSlider;
    public Image sliderFill;
    [SerializeField] public Slider[] hpSlider;
    private bool freeze = false;
    [SerializeField] public int evo1, evo2, evo3, startNum, scoreEmp = 0;
    public string saveStage;
    [SerializeField] public Missions_M scrMis;
    public DamagePanel_M damaPanel;
    public float currentPer, empTimer;
    public Color color1, color2, color3, color4;
    private CriAtomSource Sound;
    private bool HPsound;
    string score;
    //スコア強調
    public Animator animator;
    private string strGetScore = "isGetScore";

    public void Start()
    {
        scoreText.text = "$ " + 0;
        finalScoreText.text = "" + 0;
        timeText.text = "Time: " + ScoreAttack_Y.limitTime;
        epSlider.value = 0;
        ScoreAttack_Y.paramScr = this;

        if (ScoreAttack_Y.gameMode == mode.ScoreAttack) startNum = 0;
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
        Sound = GetComponent<CriAtomSource>();
        HPsound = true;
    }

    public void ScoreUpdate()      //山本加筆：publicにすることで他Scriptで参照できるようにしました
    {
        score = ScoreAttack_Y.score.ToString("N0");
        scoreText.text = "$ " + score;
        animator.SetBool(strGetScore, true);
        scoreEmp += 1;//boolのかわり
    }
    //引数で指定した分だけスコアを加算します。

    public void TimeUpdate()
    {
        if (!freeze)
        {
            timeText.text = "Time " + (int)ScoreAttack_Y.limitTime / 60 + ":" + String.Format("{0:00}", ScoreAttack_Y.limitTime % 60);
            if (ScoreAttack_Y.gameMode == mode.Result)
            {
                score = ScoreAttack_Y.score.ToString("N0");
                finalScoreText.text = "" + score;
                freeze = true;
            }
        }
    }
    //引数で指定した分だけ残りタイムを加算します。

    public void EPManager(int addEP)
    {
        if (!freeze)
        {
            ep += addEP;
            epSlider.value += addEP;
            HPManager(-10);
            mainSlider.value += 10;

            if (ep == evo1)
            {
                epSlider.value = 0;
                epSlider.maxValue = evo2 - evo1;
                hpSli[0].SetActive(false);
                hpSli[1].SetActive(true);
                hpSlider[1].value = hp;
                mainSlider = hpSlider[1];
                //TimeManager(20);
                maxHP = 250;
                hp = maxHP;
                mainSlider.value = maxHP;
            }
            else if (ep == evo2)
            {
                epSlider.value = 0;
                epSlider.maxValue = evo3 - evo2;
                hpSli[1].SetActive(false);
                hpSli[2].SetActive(true);
                hpSlider[2].value = hp;
                mainSlider = hpSlider[2];
                //TimeManager(20);
                maxHP = 500;
                hp = maxHP;
                mainSlider.value = maxHP;
            }
            else if (ep == evo3)
            {
                hpSli[3].SetActive(true);
                hpSli[2].transform.Translate(-10, 24, 0);// 場所調整
                //TimeManager(20);
                maxHP = 1000;
                hp = maxHP;
                hpSlider[2].value = 500;
                hpSlider[3].value = 500;
            }
        }
    }
    //引数で指定した分だけEPを加算します。

    public void HPManager(int addHP)
    {
        if (!freeze)
        {
            //ダメージ処理
            if (addHP > 0)
            {
                damaPanel.DamageEffect();
                hp -= addHP;
                mainSlider.value -= addHP;
            }
            if (hp <= 0)
            {
                freeze = true;
                ScoreAttack_Y.gameMode = mode.Result;
                resultPanel.SetActive(true);
                hp = 0;
                PlayerPrefs.SetString(saveStage, scrMis.load);//ミッションセーブ
                PlayerPrefs.Save();
            }

            if (hp >= maxHP)
            {
                hp = maxHP;
            }

            //バグ発生中 第四形態で一度上のゲージまで体力が減ると、回復しても下のゲージに反映されない
            if (hp >= 500 && ep >= evo3)
            {
                if (hpSlider[2].value == 500)
                {
                    mainSlider = hpSlider[3];
                }
            }
            else if (hp < 500 && ep >= evo3)
            {
                if (hpSlider[3].value == 0)
                {
                    mainSlider = hpSlider[2];
                }
            }

            if (hp < 50 && HPsound == true)
            {
                Sound.Play("LoHP");
                HPsound = false;
            }
            if (hp >= 50 && HPsound == false)
            {
                Sound?.Play("NomalHP");
                HPsound = true;
            }
        }
    }
    //引数で指定した分だけHPを加算します。

    private void Update()
    {
        TimeUpdate();

        //体力ゲージの色変換
        currentPer = epSlider.value / epSlider.maxValue;
        if (currentPer <= 0.30f)
        {
            sliderFill.color = Color.Lerp(color1, color2, currentPer * 4f);
        }
        else if (currentPer >= 0.85f)
        {
            sliderFill.color = Color.Lerp(color3, color4, currentPer);
        }
        else
        {
            sliderFill.color = Color.Lerp(color2, color3, currentPer);
        }

        if (scoreEmp >= 1)
        {
            empTimer += Time.deltaTime;
        }
        if (empTimer >= 0.5)
        {
            scoreEmp = 0;
            empTimer = 0;
            animator.SetBool(strGetScore, false);
        }
    }
}