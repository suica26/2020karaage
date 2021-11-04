using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    //カウントダウンの制御、タイマーとスコアの表示も
    public Text timerTxt, gameTimerTxt;
    public float countdown;
    int count;
    public string mode;
    public bool countSet, countFin;
    public GameObject timer3to1, limit, score, mission;
    public Pause_M pa_M;
    public Parameters_R paramR;

    void Start()
    {
        //mode = PlayerPrefs.GetString("modeJudge");
        //mode = "scoreMode";//test

        if (mode == "scoreMode")
        {
            countSet = true;
            limit.SetActive(true);
            score.SetActive(true);
            mission.SetActive(false);
            paramR.time = 300;//本当は300
        }
        else if (mode == "")
        {
            limit.SetActive(false);
            paramR.time = 1000000000;//でけぇ初期値
        }
    }

    void Update()
    {
        if (countSet && countdown >= 0)
        {
            timer3to1.SetActive(true);
            countdown -= Time.unscaledDeltaTime/2;
            count = (int)countdown;
            timerTxt.text = count.ToString();
        }

        else if (countdown <= 0)
        {
            timerTxt.text = "";
            timer3to1.SetActive(false);
            countSet = false;
            mission.SetActive(false);
            countdown = 0;
        }

        if (countSet && !countFin)
        {
            pa_M.gameSet = false;
            Time.timeScale = 0;
        }
        else if (!countSet && !countFin)
        {
            pa_M.gameSet = true;
            Time.timeScale = 1;
            countFin = true;
        }
    }
}
