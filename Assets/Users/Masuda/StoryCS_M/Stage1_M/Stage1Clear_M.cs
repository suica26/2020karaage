using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Stage1Clear_M : MonoBehaviour
{
    [SerializeField] public GameObject clear, company, next, pause, explo;
    public bool stageClear, scoreM, timeLines;
    public PlayableDirector pd;
    public float timeLineTime;
    public Text goalText;
    public Parameters_R scrParam;
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
            scrParam.maxHP = 10000;
            scrParam.hp = 10000;
            pd.Play();
            timeLines = true;
        }

        //timelineが終わる秒数のちょっと前の数字を入れる
        if (pd.time >= timeLineTime)
        {
            Cursor.visible = true;
            clear.SetActive(true);
            next.SetActive(true);
            goalText.color = new Color(0, 0, 0, 0);
            stageClear = true;
            Time.timeScale = 0;
        }
    }
}
