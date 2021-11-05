using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene_Language_M : MonoBehaviour
{
    public Text pauseP, start, howto, option, exit;
    public Text optionTitle, volume, scr, scr1, scr2, cameraS;
    public Text howTitle, key, mouse, move, jump, pause, blast, kick, cutter;
    public Text evoSen, hipSen1, hipSen2;
    public Text attention;
    public Text gameOver, retry, title;
    //stage12,stage3
    public Text clearSen, nextStage, titleBack;
    public Text clearSenC, nextStageC, titleBackC;
    //ScoreMode
    public Text mob_res_Title, chickenName, resister;
    public Text nameAtens, nameChange, yes;
    public Text rank_Title, reTry, stageChange;
    public Text sel_Stage, stage1S, stage2S, stage3S;

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
        cameraS.text = "CAMERA";
        howTitle.text = "HOW TO TITLE";
        key.text = "Keyboard";
        mouse.text = "Mouse";
        move.text = "Move";
        jump.text = "Jump";
        pause.text = "Pause";
        blast.text = "MorningBlast(Long Press)";
        kick.text = "Kick";
        cutter.text = "CrestCutter";
        evoSen.text = "When the lower gauge is full, \n the chicken will evolve";
        hipSen1.text = "HipStamp";
        hipSen2.text = "While in the air \n Let's press and hold the left click!";
        attention.text = "There seems to be nothing here";
        gameOver.text = "Game Over...";
        retry.text = "Continue";
        title.text = "Exit to title";
        mob_res_Title.text = "Title";
        chickenName.text = "Chicken Name";
        resister.text = "Register";
        nameAtens.text = "Is the name offensive, political, religious, sexual, discriminatory, or personally identifiable?";
        nameChange.text = "Change the Name";
        yes.text = "Yes";
        rank_Title.text = "Title";
        reTry.text = "Retry";
        stageChange.text = "Stage Change";
        sel_Stage.text = "Stage Select";
        stage1S.text = "Stage1";
        stage2S.text = "Stage2";
        stage3S.text = "Stage3";

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
