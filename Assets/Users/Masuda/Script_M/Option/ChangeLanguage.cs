using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    public Text start, howto, option, exit;
    public Text optionTitle, volume, scr, scr1, scr2, lan;
    public Text howTitle, key, mouse, move, jump, pause, blast, kick, cutter;
    public Text start0, start1, start2, start3, start11, start22, start33;
    public Text score, story;
    public string language;//PlayrPrefsで判別
    private SaveManager_Y saveManager;

    void Start()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager_Y>();
        PlayerPrefs.SetString("language", "Japanese");
        PlayerPrefs.Save();

        if (saveManager != null)
            switch (saveManager.GetLanguage())
            {
                case "Japanese": Change_Japanese(); break;
                case "English": Change_English(); break;
                default: break;
            }
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
        start0.text = "Beginning";
        start1.text = "Start1";
        start2.text = "Start2";
        start3.text = "Start3";
        start11.text = "Start1";
        start22.text = "Start2";
        start33.text = "Start3";
        score.text = "Score Attack";
        story.text = "Story Mode";
        PlayerPrefs.SetString("language", "English");
        language = "ENGLISH";
        if (saveManager != null)
            saveManager.SaveLanguage("English");
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
        start0.text = "初めから";
        start1.text = "ステージ1";
        start2.text = "ステージ2";
        start3.text = "ステージ3";
        start11.text = "ステージ1";
        start22.text = "ステージ2";
        start33.text = "ステージ3";
        score.text = "スコアアタック";
        story.text = "ストーリーモード";
        PlayerPrefs.SetString("language", "Japanese");
        language = "JAPANESE";
        if (saveManager != null)
            saveManager.SaveLanguage("Japanese");
    }
}
