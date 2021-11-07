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
    private float timer_TO, timer_HO;
    private bool owari;
    public Animator animator; //Abe_gameSetで新しいゲームセット
    private string TimeUpStr = "strTimeUp";
    //private string gameOWARI = "isGameSet";

    void Update()
    {
        if (ScoreAttack_Y.gameMode == mode.Result)
        {
            animator.SetBool(TimeUpStr, true);
            //Abe_gameSet.SetBool(gameOWARI, true);//sub
            owari = true;
        }

        if (owari == true)
        {
            timer_TO += Time.unscaledDeltaTime;
        }

        if (timer_TO >= 1.5f)
        {
            ImageChange();
            result.SetActive(true);
            pauseScr.gameSet = false;
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        if (paramScr.hp <= 0)
        {
            timer_HO += Time.unscaledDeltaTime;
        }

        if (timer_HO >= 1.95f)
        {
            ImageChange();
            result.SetActive(true);
            gameOver.SetActive(false);
            pauseScr.gameSet = false;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    public void ImageChange()
    {
        mainImage.sprite = wanted[evoChi.nowEvoNum];
    }
}
