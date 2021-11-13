using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    //カウントダウンの制御、タイマーとスコアの表示も
    public float countdown;
    int count;
    public bool countSet, countFin;
    public GameObject timer3to1, limit, score, mission;
    public Pause_M pauseScr;
    public Parameters_R paramScr;
    private CriAtomSource sound;
    public Animator anime;
    private string strCountDown = "isCountStart";

    void Start()
    {
        sound = GetComponent<CriAtomSource>();
        if (ScoreAttack_Y.gameMode == mode.ScoreAttack)
        {
            //ストーリーモードの設定打ち消し
            countSet = true;
            limit.SetActive(true);
            score.SetActive(true);
            mission.SetActive(false);
            timer3to1.SetActive(true);
            ScoreAttack_Y.Init();
        }
        else limit.SetActive(false);
    }

    void Update()
    {
        if (countSet)
        {
            countdown += Time.unscaledDeltaTime;
            count = (int)(countdown + 1);
            anime.SetBool(strCountDown, true);
            sound.Play();
        }
        if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            timer3to1.SetActive(false);
            countSet = false;
            anime.SetBool(strCountDown, false);
            countdown = 0;
        }

        if (countSet && !countFin)
        {
            pauseScr.gameSet = false;
            if (!ScoreAttack_Y.connecting)
                Time.timeScale = 1;
            //キャラの足を止めたい
            //鶏のスピードを0に？
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
