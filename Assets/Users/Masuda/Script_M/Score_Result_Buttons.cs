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
    [SerializeField] private Text userName;
    [SerializeField] private Text[] rankingTexts1to8;
    [SerializeField] private Text[] aroundRankingText;
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

    private void Update()
    {
        if (ScoreAttack_Y.connecting) Time.timeScale = 1f;
    }

    public void NameCheck()
    {
        //名前の確認
        nameChecker.SetActive(true);
    }

    public void NameRegister()
    {
        StartCoroutine(Connecting());
    }

    private IEnumerator Connecting()
    {
        ScoreAttack_Y.connecting = true;
        yield return StartCoroutine(Submit());
        yield return StartCoroutine(Fetch());
        yield return StartCoroutine(GetAndResistTable());
        DisplayUI();
        ScoreAttack_Y.connecting = false;
    }

    private IEnumerator Submit()
    {
        //名前を登録
        ScoreAttack_Y.SetPlayStageNum();
        ScoreAttack_Y.SubmitScore(userName.text, ScoreAttack_Y.playStageNum);
        yield return null;
    }

    private IEnumerator Fetch()
    {
        //ランキング同期
        ScoreAttack_Y.GetWorldTopScore(ScoreAttack_Y.playStageNum);
        yield return null;
        ScoreAttack_Y.FetchRank(ScoreAttack_Y.score, ScoreAttack_Y.playStageNum);
        yield return null;
        ScoreAttack_Y.GetAroundMyScores(ScoreAttack_Y.score, ScoreAttack_Y.playStageNum);
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator GetAndResistTable()
    {
        //名前とスコアをそれぞれで取得
        Debug.Log("Get Start");
        var rankNames = ScoreAttack_Y.GetNames(ScoreAttack_Y.topRanking);
        var rankScores = ScoreAttack_Y.GetScores(ScoreAttack_Y.topRanking);
        var neighborNames = ScoreAttack_Y.GetNames(ScoreAttack_Y.neighbors);
        var neighborScores = ScoreAttack_Y.GetScores(ScoreAttack_Y.neighbors);
        Debug.Log("Get Complete");
        yield return null;

        Debug.Log("Resist Start");
        for (int i = 0; i < rankNames.Length; i++)
        {
            rankingTexts1to8[i].text = $"{rankNames[i]} {rankScores[i]}";
        }
        aroundRankingText[0].text = $"{neighborNames[0]} {neighborScores[0]}";
        aroundRankingText[1].text = $"{neighborNames[2]} {neighborScores[2]}";
        aroundRankingText[2].text = $"{neighborNames[1]} {neighborScores[1]}";
        Debug.Log("Resist Complete");
    }

    private void DisplayUI()
    {
        nameChecker.SetActive(false);
        nameDeal.SetActive(false);
        ranking.SetActive(true);
        Debug.Log("Ranking Complete");
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
        ScoreAttack_Y.gameMode = mode.ScoreAttack;
        loadPanel.SetActive(true);
        SceneManager.LoadScene(nowSceneName);
    }

    public void ReStartStage(int stageNum)
    {
        //選んだステージを始める
        ScoreAttack_Y.gameMode = mode.ScoreAttack;
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
