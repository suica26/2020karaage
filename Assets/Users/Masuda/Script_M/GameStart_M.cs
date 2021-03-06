﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart_M : MonoBehaviour
{
    //タイトル画面でボタンをクリックするとゲーム画面を読み込みます
    //シーンGameStartのButtonに張っ付いてます
    [SerializeField] public GameObject load;
    public string sceneName;

    public void OnStart()
    {
        load.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}
