using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions_M : MonoBehaviour
{
    [SerializeField] public Text mission, submis, exmis, count, per, tips;
    [SerializeField]
    public GameObject player, shop, misBox, company, hip,
        buildTips, tipsChicken;
    [SerializeField] public TextAsset txtFile;
    [SerializeField] public int smallNum, bigNum, achieve;
    [SerializeField]
    public int smallBorder1, smallBorder2, smallBorder3,
                                bigBorder1, bigBorder2, bigBorder3, bigBorder4;
    [SerializeField] public int manhole, hydrant;
    public bool first = true, second = false, third = false, fourth = false, final = false,
        hipStamp = false, tip = false;
    public string txtData;
    public string[] splitText;
    [SerializeField] public Animation missionSlide;
    [SerializeField] public float timer, tipsTimer;
    //public bool mission1, mission2, mission3;

    [SerializeField] public ObjectStateManagement_Y eneBillScr;
    
    public void Start()
    {
        txtData = txtFile.text;
        splitText = txtData.Split(char.Parse("\n"));
        misBox.SetActive(false);
        hip.SetActive(false);
        buildTips.SetActive(false);
        tipsChicken.SetActive(false);
    }

    void Update()
    {
        
    }
}
