using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene_Language_M : MonoBehaviour
{
    public Text pauseP, start, howto, option, exit;
    public Text optionTitle, volume, scr, scr1, scr2;
    public Text howTitle, key, mouse, move, jump, pause, blast, kick, cutter;
    public Text evoSen, hipSen1,hipSen2;
    public Text attention;
    public Text gameOver, retry, title;
    //stage12,stage3
    public Text clearSen, nextStage, titleBack;
    public Text clearSenC, nextStageC, titleBackC;

    public bool stage3, mobileMode;
    private string languageMode;
    void Start()
    {
        languageMode = PlayerPrefs.GetString("language");
        if (languageMode == "Japanese" && !mobileMode)
        {
            return;
            //Japanese();
        }
        else if (languageMode == "Japanese" && mobileMode)
        {
            hipSen2.text = "空中にいる間に、\nキックボタンを長押ししよう";
        }
        else if (languageMode == "English" && !mobileMode)
        {
            English();
        }
        else if (languageMode == "English" && mobileMode)
        {
            English();
            hipSen2.text = "While in the air \n Let's press and hold the kick button!";
        }
    }

    void Japanese()
    {

    }

    void English()
    {
        //英語モードに移行
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
        evoSen.text = "When the upper left gauge is full, \n the chicken will evolve";
        hipSen1.text = "HipStamp";
        hipSen2.text = "While in the air \n Let's press and hold the left click!";
        attention.text = "There seems to be nothing here";
        gameOver.text = "Game Over...";
        retry.text = "Continue";
        title.text = "Exit to title";

        if (stage3)
        {
            clearSenC.text = "Storymode Clear！";
            nextStageC.text = "One more play";
            titleBackC.text = "Exit to title";
        }
        else if (!stage3)
        {
            clearSen.text = "Stage clear！";
            nextStage.text = "Next stage";
            titleBack.text = "Exit to title";
        }
    }
}
