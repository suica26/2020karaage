using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score_Result_Buttons : MonoBehaviour
{
    public GameObject nameDeal, nameChecker, stageSllect, ranking, loadPanel;
    public string sceneName, nowSceneName, lanMode, gameMode;
    public string [] scenes;
    public Text stageName;
    public Image blast;
    
    void Start()
    {
        //スコアモードのボタン操作を管理＋その他仕様
        nowSceneName = SceneManager.GetActiveScene().name;
        lanMode = PlayerPrefs.GetString("language");
        gameMode = PlayerPrefs.GetString("modeJudge");

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
        else if (nowSceneName == "stage1" && lanMode == "English")
        {
            stageName.text = "Stage１\nいなかの町";//決まってない
        }
        else if (nowSceneName == "Stage2" && lanMode == "English")
        {
            stageName.text = "Stage２\n海ぞいの町";
        }
        else if (nowSceneName == "Stage3" && lanMode == "English")
        {
            stageName.text = "Stage３\nさいごの街";
        }

        if (gameMode == "scoreMode" && nowSceneName == "stage1")
        {
            blast.color = new Color(255,255,255,130);
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
