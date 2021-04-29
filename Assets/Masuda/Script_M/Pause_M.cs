﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_M : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel,optionPanel;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //ポーズのオンオフ
            pausePanel.SetActive(!pausePanel.activeSelf);

            //ポーズ中に停止
            if (pausePanel.activeSelf)
            {
                Time.timeScale = 0f;
            }
            //時が動き出す
            else
            {
                Time.timeScale = 1f;
                optionPanel.SetActive(false);
            }
        }
    }
}
