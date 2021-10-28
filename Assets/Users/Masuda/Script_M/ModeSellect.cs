using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSellect : MonoBehaviour
{
    public GameObject modeSellect, stageSellect;
    public string modeJ;
    private new CriAtomSource audio;

    //modeJudgeにスコアアタックかどうかの記録を残す
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
        PlayerPrefs.SetString("modeJudge", "");
    }

    void Update()
    {
        
    }

    public void OnStory()
    {
        PlayerPrefs.SetString("modeJudge", "");
        modeJ = PlayerPrefs.GetString("modeJudge");
        modeSellect.SetActive(false);
        stageSellect.SetActive(true);
    }

    public void OnScore()
    {
        PlayerPrefs.SetString("modeJudge", "scoreMode");
        modeJ = PlayerPrefs.GetString("modeJudge");
        modeSellect.SetActive(false);
        stageSellect.SetActive(true);
    }

}
