using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stage0Lan_M : MonoBehaviour
{
    public Text pauseP, start, howto, option, exit;
    public Text optionTitle, volume, scr, scr1, scr2;
    public Text howTitle, key, mouse, move, jump, pause, blast, kick, cutter;
    private string languageMode;
    void Start()
    {
        languageMode = PlayerPrefs.GetString("language");
        if (languageMode == "Japanese")
        {
            return;
            //Japanese();
        }
        else if (languageMode == "English")
        {
            ToEnglish();
        }
    }

    void ToEnglish()
    {
        pauseP.text = "PAUSE";
        start.text = "RESUME";
        howto.text = "HOW TO PLAY";
        option.text = "OPTION";
        exit.text = "EXIT TO TITLE";
        optionTitle.text = "OPTION";
        volume.text = "VOLUME";
        scr.text = "QUALITY";
        scr1.text = "LOW";
        scr2.text = "HIGH";
        howTitle.text = "HOW TO TITLE";
        key.text = "Keyboard";
        mouse.text = "Mouse";
        move.text = "Move";
        jump.text = "Jump";
        pause.text = "Pause";
        blast.text = "MorningBlast(Long Press)";
        kick.text = "Kick";
        cutter.text = "CrestCutter";
    }
}
