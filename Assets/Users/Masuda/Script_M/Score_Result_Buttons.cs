using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score_Result_Buttons : MonoBehaviour
{
    public GameObject nameDeal, nameChecker, stageSllect, ranking, loadPanel;
    public string sceneName, nowSceneName;
    public string [] scenes;
    public Text stageName;
    
    void Start()
    {
        nowSceneName = SceneManager.GetActiveScene().name;
        if (nowSceneName == "stage1")
        {
            stageName.text = "ステージ１\nいなかの町";
        }
        else if (nowSceneName == "Stage2")
        {
            stageName.text = "ステージ２\n海ぞいの町";
        }
        else if (nowSceneName == "Stage3")
        {
            stageName.text = "ステージ３\nさいごの街";
        }
    }

    
    void Update()
    {
        
    }

    public void NameCheck()
    {
        //名前の確認
        nameChecker.SetActive(true);
    }

    public void NameRegister()
    {
        //名前を登録
        //名前をサーバー？に送る処理
        nameChecker.SetActive(false);
        nameDeal.SetActive(false);
        ranking.SetActive(true);
    }

    public void NameChange()
    {
        //名前を変更
        nameDeal.SetActive(false);
    }

    public void SellectScenes()
    {
        //ステージ選択パネル表示
        stageSllect.SetActive(true);
    }

    public void RetryNowScene()
    {
        //今いるシーンをやり直す
        loadPanel.SetActive(true);
        SceneManager.LoadScene(nowSceneName);
    }

    public void ReStartSt1()
    {
        //選んだステージを始める
        loadPanel.SetActive(true);
        SceneManager.LoadScene(scenes[0]);
    }

    public void ReStartSt2()
    {
        //選んだステージを始める
        loadPanel.SetActive(true);
        SceneManager.LoadScene(scenes[1]);
    }

    public void ReStartSt3()
    {
        //選んだステージを始める
        loadPanel.SetActive(true);
        SceneManager.LoadScene(scenes[2]);
    }

    public void TitleBack()
    {
        //タイトルに戻る
        loadPanel.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}
