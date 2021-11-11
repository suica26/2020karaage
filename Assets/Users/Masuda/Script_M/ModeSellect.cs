using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSellect : MonoBehaviour
{
    public GameObject modeSellect, stageSelect1, stageSelect2;
    public string modeJ;
    private CriAtomSource criAtomSource;

    //modeJudgeにスコアアタックかどうかの記録を残す
    void Start()
    {
        criAtomSource = GetComponent<CriAtomSource>();
        PlayerPrefs.SetString("modeJudge", "");
    }

    public void OnStory()
    {
        PlayerPrefs.SetString("modeJudge", "");
        modeJ = PlayerPrefs.GetString("modeJudge");
        modeSellect.SetActive(false);
        stageSelect1.SetActive(true);
        ScoreAttack_Y.gameMode = mode.Story;
    }

    public void OnScore()
    {
        PlayerPrefs.SetString("modeJudge", "scoreMode");
        modeJ = PlayerPrefs.GetString("modeJudge");
        modeSellect.SetActive(false);
        stageSelect2.SetActive(true);
        ScoreAttack_Y.Init();
        ScoreAttack_Y.gameMode = mode.ScoreAttack;
    }
}
