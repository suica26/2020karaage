using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleGameStart_M : MonoBehaviour
{
    public GameObject gameMode, start, loadPanel;
    public string cleared, sceneName;
    private new CriAtomSource audio;

    void Start()
    {
        //cleared = PlayerPrefs.GetString("storyClear");
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    public void OnClick()
    {
        audio.Play("System_Decision");
        //loadPanel.SetActive(true);
        gameMode.SetActive(true);
        start.SetActive(false);
        //SceneManager.LoadScene(sceneName);

        /*if (cleared == "clear")
        {
            gameMode.SetActive(true);
            start.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }*/
    }
}
