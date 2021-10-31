using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_TimeOut : MonoBehaviour
{
    //時間切れと体力切れした場合に、手配書画面が出るように
    public GameObject result, gameOver;
    public Parameters_R paramR;
    public Pause_M pa_M;

    void Start()
    {
        
    }

    void Update()
    {
        if (paramR.time <= 0)
        {
            result.SetActive(true);
            pa_M.gameSet = false;
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        if (paramR.hp <= 0)
        {
            result.SetActive(true);
            gameOver.SetActive(false);
            pa_M.gameSet = false;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
}
