using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    public Text start, howto, option, exit;
    public Text optionTitle, volume, scr, scr1, scr2, lan;
    public Text howTitle, key, mouse, move, jump, pause, blast, kick, cutter;
    public string language;//PlayrPrefsで判別

    void Start()
    {
        PlayerPrefs.SetString("language", "Japanese");
        PlayerPrefs.Save();
    }

    public void Change_English()
    {
        //英語モードに移行
        start.text = "START";
        howto.text = "HOW TO PLAY";
        option.text = "OPTION";
        exit.text = "EXIT";
        optionTitle.text = "OPTION";
        volume.text = "VOLUME";
        scr.text = "QUALITY";
        scr1.text = "LOW";
        scr2.text = "HIGH";
        lan.text = "LANGUAGE";
        howTitle.text = "HOW TO TITLE";
        key.text = "Keyboard";
        mouse.text = "Mouse";
        move.text = "Move";
        jump.text = "Jump";
        pause.text = "Pause";
        blast.text = "MorningBlast(Long Press)";
        kick.text = "Kick";
        cutter.text = "CrestCutter";
        PlayerPrefs.SetString("language", "English");
        language = "ENGLISH";
    }

    public void Change_Japanese()
    {
        //日本語モードに移行
        start.text = "スタート";
        howto.text = "暴れかた";
        option.text = "オプション";
        exit.text = "ゲーム終了";
        optionTitle.text = "オプション";
        volume.text = "音量";
        scr.text = "画質";
        scr1.text = "低い";
        scr2.text = "高い";
        lan.text = "言語";
        howTitle.text = "暴れかた";
        key.text = "キーボード";
        mouse.text = "マウス";
        move.text = "移動";
        jump.text = "ジャンプ";
        pause.text = "ポーズ";
        blast.text = "おはようブラスト(長押し)";
        kick.text = "キック";
        cutter.text = "カッター";
        PlayerPrefs.SetString("language", "Japanese");
        language = "JAPANESE";
    }

}
