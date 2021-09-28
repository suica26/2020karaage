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

    private void Start()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager_Y>();
    }

    private void Update()
    {
        if (stageNum > 0)
        {
            if (!saveManager.GetStageFlg(stageNum)) gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }
    }

    public void OnStart()
    {
        load.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}
