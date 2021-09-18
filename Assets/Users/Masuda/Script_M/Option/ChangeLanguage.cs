using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    public Text start, howto, option, exit;
    public string language;//PlayrPrefsで判別
    public GameObject option_E, option_J;

    void Start()
    {
        
    }

    public void Change_English()
    {
        //英語モードに移行
        start.text = "START";
        howto.text = "HOW TO PLAY";
        option.text = "OPTION";
        exit.text = "EXIT";
        PlayerPrefs.SetString(language, "English");
        option_J.SetActive(false);
        option_E.SetActive(true);
    }

    public void Change_Japanese()
    {
        //日本語モードに移行
        start.text = "スタート";
        howto.text = "暴れかた";
        option.text = "オプション";
        exit.text = "ゲーム終了";
        language = "Japanese";
        PlayerPrefs.SetString(language, "Japanese");
        option_J.SetActive(true);
        option_E.SetActive(false);
    }

}
