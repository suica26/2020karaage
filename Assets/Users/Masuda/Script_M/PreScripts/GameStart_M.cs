using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart_M : MonoBehaviour
{
    //タイトル画面でボタンをクリックするとゲーム画面を読み込みます
    //シーンGameStartのButtonに張っ付いてます
    [SerializeField] public GameObject load;
    public string sceneName;
    private SaveManager_Y saveManager;
    public int stageNum;
    private delegate bool CheckFunc(int stageNum);
    private CheckFunc checker;

    private void Start()
    {
        saveManager = SaveManager_Y.GetInstance();
    }

    private void Update()
    {
        if (ScoreAttack_Y.gameMode == mode.Story)
            checker = saveManager.GetStageFlg;
        else if (ScoreAttack_Y.gameMode == mode.ScoreAttack)
            checker = saveManager.GetClearFlg;

        if (stageNum > 0)
        {
            if (!checker(stageNum)) gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }
    }

    public void OnStart()
    {
        load.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}
