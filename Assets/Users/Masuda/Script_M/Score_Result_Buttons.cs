using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score_Result_Buttons : MonoBehaviour
{
    public GameObject nameDeal, nameChecker, stageSelect, ranking, loadPanel;
    public string sceneName, nowSceneName, lanMode, gameMode;
    public string[] scenes;
    public Text stageName;
    public Image blast;
    private Dictionary<string, string> stageNameJ = new Dictionary<string, string>()
    {
        {"stage1","ステージ１\nいなかの町"},
        {"Stage2","ステージ２\n海ぞいの町"},
        {"Stage3","ステージ３\nさいごの街"}
    };
    private Dictionary<string, string> stageNameE = new Dictionary<string, string>()
    {
        {"stage1","Stage１\nいなかの町"},
        {"Stage2","Stage２\n海ぞいの町"},
        {"Stage3","Stage３\nさいごの街"}
    };

    void Start()
    {
        //スコアモードのボタン操作を管理＋その他仕様
        nowSceneName = SceneManager.GetActiveScene().name;
        lanMode = PlayerPrefs.GetString("language");
        gameMode = PlayerPrefs.GetString("modeJudge");

        if (lanMode == "English") stageName.text = stageNameE[nowSceneName];
        else stageName.text = stageNameJ[nowSceneName];

        if (ScoreAttack_Y.gameMode == mode.ScoreAttack && nowSceneName == "stage1")
        {
            blast.color = new Color(255, 255, 255, 130);
        }
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

    public void SelectScenes()
    {
        //ステージ選択パネル表示
        stageSelect.SetActive(true);
    }

    public void RetryNowScene()
    {
        //今いるシーンをやり直す
        loadPanel.SetActive(true);
        SceneManager.LoadScene(nowSceneName);
    }

    public void ReStartStage(int stageNum)
    {
        //選んだステージを始める
        loadPanel.SetActive(true);
        SceneManager.LoadScene(scenes[stageNum]);
    }

    public void TitleBack()
    {
        //タイトルに戻る
        loadPanel.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}
