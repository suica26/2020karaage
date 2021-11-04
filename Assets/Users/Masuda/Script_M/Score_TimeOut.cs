using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_TimeOut : MonoBehaviour
{
    //時間切れと体力切れした場合に、手配書画面が出るように
    public GameObject result, gameOver, newRecord, timeUp;
    public Parameters_R paramR;
    public Pause_M pa_M;
    public Image mainImage;
    public Sprite[] wanted;
    public EvolutionChicken_R evoChi;
    private float timer_TO, timer_HO;
    public Animator animator;
    private string TimeUpStr = "strTimeUp";

    void Start()
    {
        
    }

    void Update()
    {
        if (paramR.time <= 0)
        {
            animator.SetBool(TimeUpStr, true);
            timer_TO += Time.unscaledDeltaTime;
        }

        if (timer_TO >= 1.0f)
        {
            ImageChange();
            result.SetActive(true);
            pa_M.gameSet = false;
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        if (paramR.hp <= 0)
        {
            timer_HO += Time.unscaledDeltaTime;
        }

        if (timer_HO >= 1.95f)
        {
            ImageChange();
            result.SetActive(true);
            gameOver.SetActive(false);
            pa_M.gameSet = false;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    public void ImageChange()
    {
        if (evoChi.nowEvoNum == 0)
        {
            mainImage.sprite = wanted[0];
        }
        else if (evoChi.nowEvoNum == 1)
        {
            mainImage.sprite = wanted[1];
        }
        else if (evoChi.nowEvoNum == 2)
        {
            mainImage.sprite = wanted[2];
        }
        else if (evoChi.nowEvoNum == 3)
        {
            mainImage.sprite = wanted[3];
        }
    }
}
