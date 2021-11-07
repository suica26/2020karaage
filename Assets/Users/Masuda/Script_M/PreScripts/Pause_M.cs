using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_M : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel, optionPanel, how;
    private new CriAtomSource audio;
    public bool gameSet;
    public Parameters_R para;
    public Stage1Clear_M s1Cle;
    private mode nowGameMode;

    void Start()
    {
        pausePanel.SetActive(false);
        audio = GetComponent<CriAtomSource>();
    }

    void Update()
    {
        if (para.hp <= 0 || s1Cle?.stageClear == true)
        {
            gameSet = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameSet)
        {
            PauseStart();
        }
    }

    public void PauseStart()
    {
        //ポーズのオンオフ
        pausePanel.SetActive(!pausePanel.activeSelf);
        //how.SetActive(!pausePanel.activeSelf);
        audio?.Play("System_Decision");

        //ポーズ中に停止
        if (pausePanel.activeSelf)
        {
            nowGameMode = ScoreAttack_Y.gameMode;
            Time.timeScale = 0f;
            ScoreAttack_Y.gameMode = mode.Pause;
            Cursor.visible = true;
        }

        //時が動き出す
        if (!pausePanel.activeSelf)
        {
            Time.timeScale = 1f;
            ScoreAttack_Y.gameMode = nowGameMode;
            optionPanel.SetActive(false);
            how.SetActive(false);
            Cursor.visible = false;
        }
    }
}
