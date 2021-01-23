﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_M : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    
    void Start()
    {
        //ポーズパネルの設定
        pausePanel.SetActive(false);
        resumeButton.onClick.AddListener(Resume);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (pausePanel.activeSelf == false)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
        }
    }

    private void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
