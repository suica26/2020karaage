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
    public bool countSet, countFin;
    public GameObject timer3to1, limit, score, mission;
    public Pause_M pauseScr;
    public Parameters_R paramScr;

    void Start()
    {
        if (ScoreAttack_Y.gameMode == mode.ScoreAttack)
        {
            countSet = true;
            limit.SetActive(true);
            score.SetActive(true);
            mission.SetActive(false);
            ScoreAttack_Y.Init();
        }
        else limit.SetActive(false);
    }

    void Update()
    {
        if (countSet && countdown >= 0)
        {
            timer3to1.SetActive(true);
            countdown -= Time.unscaledDeltaTime / 2;
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
            pauseScr.gameSet = false;
            Time.timeScale = 0;
        }
        else if (!countSet && !countFin)
        {
            pauseScr.gameSet = true;
            Time.timeScale = 1;
            countFin = true;
            ScoreAttack_Y.countDown = false;
        }
    }
}
