using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleGameStart_M : MonoBehaviour
{
    public GameObject gameMode, story, start, loadPanel;
    public string cleared;
    private new CriAtomSource audio;

    void Start()
    {
        //cleared = PlayerPrefs.GetString("storyClear");
        audio = GetComponent<CriAtomSource>();
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
        audio.Play("System_Decision");
        if (cleared == "clear")
        {
            gameMode.SetActive(true);
            start.SetActive(false);
        }
        else
        {
            story.SetActive(true);
            start.SetActive(false);
        }
    }
}
