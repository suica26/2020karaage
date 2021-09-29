using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3_M : Missions_M
{
    public string onLoad_s3;
    public GameObject eneBill;
    public float evoTimer;
    public GameObject bossIcon;
    public override void Start()
    {
        base.Start();
        misBox.SetActive(true);
        Time.timeScale = 1;
        onLoad_s3 = PlayerPrefs.GetString(scrParame.saveStage, "");
        switch (onLoad_s3)
        {
            case "first": FirstMission_3(); break;
            case "second": SecondMission_3(); break;
            default: FirstMission_3(); break;
        }
        PlayerPrefs.DeleteKey(onLoad_s3);
    }

    // Update is called once per frame
    void Update()
    {
        evoNum = scrEvoChi.EvolutionNum;
        if (evoNum >= 3 && first)
        {
            evoTimer += Time.deltaTime;
        }

        if (!eneBill)
        {
            //PlayerPrefs.SetString("storyClear", "clear");
        }

        if (first && evoTimer >= 1.0f)
        {
            SecondMission_3();
        }
    }

    public void FirstMission_3()
    {
        missionSlide.Play();
        mission.text = splitText[0];
        submis.text = splitText[1];
        exmis.text = splitText[2];
        //per.text = "0%";
        load = "first";
        first = true;
    }

    public void SecondMission_3()
    {
        //支部攻撃
        eneBillScr.changeDamageFlg();
        bossIcon.SetActive(true);

        missionSlide.Play();
        mission.text = splitText[3];
        submis.text = splitText[4];
        exmis.text = splitText[5];
        load = "second";
        first = false;
        second = true;
    }

    public void ThirdMission_3()
    {
        //敵破壊1
        missionSlide.Play();
        mission.text = "";
        submis.text = splitText[6];
        exmis.text = splitText[7];
        second = false;
        third = true;
    }

    public void FourthMission_3()
    {
        //支部攻撃2
        missionSlide.Play();
        submis.text = splitText[8];
        exmis.text = splitText[9];
        third = false;
        fourth = true;
    }

    public void FiveMission_3()
    {
        //敵破壊2
        missionSlide.Play();
        submis.text = splitText[10];
        exmis.text = splitText[11]; 
        fourth = false;
        five = true;
    }

    public void SixMission_3()
    {
        //支部撃破
        missionSlide.Play();
        submis.text = splitText[12];
        exmis.text = splitText[13];
        five = false;
        six = true;
    }
}
