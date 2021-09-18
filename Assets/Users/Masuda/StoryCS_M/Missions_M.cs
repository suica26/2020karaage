using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions_M : MonoBehaviour
{
    [SerializeField] public Text mission, submis, exmis, per, tips;
    [SerializeField]
    public GameObject player, misBox, company, tipsChicken;
    [SerializeField] public TextAsset txtFile;//英語ファイルを追加し、mainFileで日英のファイル変更を可能に
    [SerializeField] public int smallNum, bigNum, achieve, hitID;
    [SerializeField]
    public int smallBorder1, smallBorder2, smallBorder3,
                                bigBorder1, bigBorder2, bigBorder3, bigBorder4;
    public bool first = false, second = false, third = false, fourth = false, five = false,
        six = false, seven = false, final = false, tip = false;
    public string txtData;
    public string[] splitText;
    [SerializeField] public Animation missionSlide;
    [SerializeField] public float timer, tipsTimer;
    [SerializeField] public ObjectStateManagement_Y eneBillScr;
    public bool stage2;
    public Parameters_R scrParame;
    public string load;
    public EvolutionChicken_R scrEvoChi;
    public int evoNum;

    public virtual void Start()
    {
        txtData = txtFile.text;
        splitText = txtData.Split(char.Parse("\n"));
        //misBox.SetActive(false);
        tipsChicken.SetActive(false);
    }

    public virtual void BigNumberPlus()
    {
        if (second && !stage2)
        {
            bigNum++;
            achieve += 20;
            per.text = achieve + "%";
        }

        else if (first && stage2)
        {

            if (hitID == 3)
            {
                bigNum++;
                //anythingCount += 1;
                hitID = 0;
                achieve += 14;
                per.text = achieve + "%";
            }
            else
            {
                hitID = 0;
            }
        }
    }
    public virtual void SmallNumberPlus()
    {
        if (second && !stage2)
        {
            smallNum++;
            achieve += 4;
            per.text = achieve + "%";
        }

        else if (first && stage2)
        {
            if (hitID == 3)
            {
                smallNum++;
                //anythingCount += 1;
                hitID = 0;
                achieve += 3;
                per.text = achieve + "%";
            }
            else
            {
                hitID = 0;
            }
        }
    }

}



