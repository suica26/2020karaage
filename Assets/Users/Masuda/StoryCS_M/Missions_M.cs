using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions_M : MonoBehaviour
{
    [SerializeField] public Text mission, submis, exmis, per, tips;
    [SerializeField]
    public GameObject player, misBox, company, tipsChicken, shibuLight;
    [SerializeField] public TextAsset txtFile, japanese, english;//英語ファイルを追加し、mainFileで日英のファイル変更を可能に
    [SerializeField] public int smallNum, bigNum, achieve, hitID;
    [SerializeField]
    public int smallBorder1, smallBorder2, smallBorder3,
                                bigBorder1, bigBorder2, bigBorder3, bigBorder4;
    public bool first = false, second = false, third = false, fourth = false, five = false,
        six = false, seven = false, final = false, tip = false, another = false;
    public string strTxtFile;
    public string[] splits;
    [SerializeField] public Animation missionSlide;
    [SerializeField] public float timer, tipsTimer;
    [SerializeField] public ObjectStateManagement_Y eneBillScr;
    public Parameters_R scrParame;
    public string load, playLanguage;
    public EvolutionChicken_R scrEvoChi;
    public int evoNum;

    public virtual void Start()
    {
        playLanguage = PlayerPrefs.GetString("language");
        if (playLanguage == "English")
        {
            txtFile = english;
        }
        else if (playLanguage == "Japanese")
        {
            txtFile = japanese;
        }
        strTxtFile = txtFile.text;
        splits = strTxtFile.Split(char.Parse("\n"));
        //misBox.SetActive(false);
        tipsChicken.SetActive(false);
        shibuLight = GameObject.Find("Light_pillar");
        shibuLight.SetActive(false);
    }

    public virtual void BigNumberPlus() { }
    public virtual void SmallNumberPlus() { }
}