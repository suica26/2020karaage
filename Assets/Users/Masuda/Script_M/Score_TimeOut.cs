using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_TimeOut : MonoBehaviour
{
    //時間切れと体力切れした場合に、手配書画面が出るように
    public GameObject result, gameOver, newRecord, timeUp;
    public Parameters_R paramScr;
    public Pause_M pauseScr;
    public Image mainImage;
    public Sprite[] wanted;
    public EvolutionChicken_R evoChi;
    [SerializeField] private float timer_TO, timer_HO;
    public Animator animator;
    private string TimeUpStr = "strTimeUp";
    private int stageNum;

    private void Start()
    {
        ScoreAttack_Y.SetPlayStageNum();
    }

    void Update()
    {
        if (ScoreAttack_Y.gameMode == mode.Result)
        {
            animator.SetBool(TimeUpStr, true);
            timer_TO += Time.unscaledDeltaTime;
        }

        if (timer_TO >= 2f)
        {
            ImageChange();
            result.SetActive(true);
            if (ScoreAttack_Y.CheckNewRecord(ScoreAttack_Y.playStageNum)) newRecord.SetActive(true);
            pauseScr.gameSet = false;
            Cursor.visible = true;
            if (!ScoreAttack_Y.connecting) Time.timeScale = 0;
        }

        if (paramScr.hp <= 0)
        {
            timer_HO += Time.unscaledDeltaTime;
        }

        if (timer_HO >= 2f)
        {
            ImageChange();
            result.SetActive(true);
            gameOver.SetActive(false);
            pauseScr.gameSet = false;
            Cursor.visible = true;
            if (!ScoreAttack_Y.connecting)
                Time.timeScale = 0;
        }
    }

    public void ImageChange()
    {
        mainImage.sprite = wanted[evoChi.nowEvoNum];
    }
}
