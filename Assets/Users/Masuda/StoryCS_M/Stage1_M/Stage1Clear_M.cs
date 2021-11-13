using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Stage1Clear_M : MonoBehaviour
{
    [SerializeField] public GameObject clear, company, next, pause, explo, goalTxt;
    public bool stageClear, scoreM, timeLines;
    public PlayableDirector pd;
    public float timeLineTime;
    void Start()
    {
        clear.SetActive(false);
        next.SetActive(false);
        explo.SetActive(false);
        pd.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreAttack_Y.gameMode == mode.ScoreAttack)
        {
            scoreM = true;
        }

        if (!company && !stageClear && !scoreM)
        {
            //Time.timeScale = 0;         
            explo.SetActive(true);
            pd.Play();
            timeLines = true;
        }

        //timelineが終わる秒数のちょっと前の数字を入れる
        if (pd.time >= timeLineTime)
        {
            Cursor.visible = true;
            clear.SetActive(true);
            next.SetActive(true);
            goalTxt.SetActive(false);
            stageClear = true;
            Time.timeScale = 0;
        }
    }
}
