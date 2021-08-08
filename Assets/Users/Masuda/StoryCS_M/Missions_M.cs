using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions_M : MonoBehaviour
{
    [SerializeField] public Text mission, submis, exmis, count, per, tips, mainCount;
    [SerializeField]
    public GameObject player, shop, misBox, company, buildTips, tipsChicken;
    [SerializeField] public TextAsset txtFile;
    [SerializeField] public int smallNum, bigNum, achieve, main;
    [SerializeField]
    public int smallBorder1, smallBorder2, smallBorder3,
                                bigBorder1, bigBorder2, bigBorder3, bigBorder4;
    public bool first = true, second = false, third = false, fourth = false, final = false, tip = false;
    public string txtData;
    public string[] splitText;
    [SerializeField] public Animation missionSlide;
    [SerializeField] public float timer, tipsTimer;

    [SerializeField] public ObjectStateManagement_Y eneBillScr;
    
    public virtual void Start()
    {
        mainCount.text += main;
        txtData = txtFile.text;
        splitText = txtData.Split(char.Parse("\n"));
        //misBox.SetActive(false);
        buildTips.SetActive(false);
        tipsChicken.SetActive(false);
    }

    public virtual void BigNumberPlus()
    {
        bigNum++;
        if (second || first)
        {
            achieve += 20;
            per.text = achieve + "%";
        }
    }
    public virtual void SmallNumberPlus()
    {
        smallNum++;
        if (second || first)
        {
            achieve += 4;
            per.text = achieve + "%";
        }
    }
}



