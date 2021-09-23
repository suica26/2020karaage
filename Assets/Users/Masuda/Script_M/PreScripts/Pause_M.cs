using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_M : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel, optionPanel, how;
    //, gameOver
    private new CriAtomSource audio;
    private bool gameSet;
    void Start()
    {
        pausePanel.SetActive(false);
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseStart();
        }
        /*if (gameOver)
        {
            gameSet = false;
        }*/
    }

    public void PauseStart()
    {
        //ポーズのオンオフ
        pausePanel.SetActive(!pausePanel.activeSelf);
        //how.SetActive(!pausePanel.activeSelf);
        audio.Play("System_Decision");

        //ポーズ中に停止
        if (pausePanel.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
        //時が動き出す
        else
        {
            Time.timeScale = 1f;
            optionPanel.SetActive(false);
            how.SetActive(false);
            Cursor.visible = false;
        }
    }
}
