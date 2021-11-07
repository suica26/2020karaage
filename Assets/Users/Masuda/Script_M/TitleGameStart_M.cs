using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleGameStart_M : MonoBehaviour
{
    public GameObject gameMode, story, start, loadPanel;
    public string cleared;
    private CriAtomSource criAtomSource;
    private SaveManager_Y saveManager;

    void Start()
    {
        //cleared = PlayerPrefs.GetString("storyClear");
        criAtomSource = GetComponent<CriAtomSource>();
        saveManager = SaveManager_Y.GetInstance();
    }

    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.L))
        {
            cleared = "clear";
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            cleared = "";
        }
    }

    public void OnClick()
    {
        criAtomSource?.Play("System_Decision");
        if (saveManager.GetClearFlg(3))
        {
            gameMode.SetActive(true);
            start.SetActive(false);
        }
        else
        {
            story.SetActive(true);
            start.SetActive(false);
            ScoreAttack_Y.gameMode = mode.Story;
        }
    }
}
