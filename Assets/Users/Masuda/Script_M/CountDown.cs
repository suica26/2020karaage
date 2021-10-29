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

    void Start()
    {
        //mode = PlayerPrefs.GetString("modeJudge");
    }

    void Update()
    {
        if (mode == "scoreMode")
        {
            countSet = true;
            limit.SetActive(true);
            score.SetActive(true);
            mission.SetActive(false);
            gameTimerTxt.text = "300";
        }

        if (countSet && countdown >= 0)
        {
            timer3to1.SetActive(true);
            countdown -= Time.unscaledDeltaTime;
            count = (int)countdown;
            timerTxt.text = count.ToString();
        }

        else if (countdown <= 0)
        {
            timerTxt.text = "";
            timer3to1.SetActive(false);
            countSet = false;
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
