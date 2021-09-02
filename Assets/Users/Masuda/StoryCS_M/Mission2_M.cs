using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission2_M : Missions_M
{
    public int gasStand, gasTank;
    public string onLoad;
    public float timer_2_1;
    public GameObject bossIcon;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        misBox.SetActive(true);
        Time.timeScale = 1;
        onLoad = PlayerPrefs.GetString(scrParame.saveStage, "");
        switch (onLoad)
        {
            case "first": FirstMission_2(); break;
            case "second": SecondMission_2(); break;
            case "third": ThirdMission_2(); break;
            case "fourth": FourthMission_2(); break;
            case "five": FiveMission_2(); break;
            case "final": FinalMission_2(); break;
            default: FirstMission_2(); break;
        }
        PlayerPrefs.DeleteKey(scrParame.saveStage);
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 comPos = company.transform.position;
        float dis = Vector3.Distance(playerPos, comPos);
        evoNum = scrEvoChi.EvolutionNum;

        if (bigNum >= bigBorder4 && first == true)
        {
            SecondMission_2();
        }
        else if (bigNum >= bigBorder3 && smallNum >= smallBorder1 && first == true)
        {
            SecondMission_2();
        }
        else if (bigNum >= bigBorder2 && smallNum >= smallBorder2 && first == true)
        {
            SecondMission_2();
        }
        else if (bigNum >= bigBorder1 && smallNum >= smallBorder3 && first == true)
        {
            SecondMission_2();
        }

        if (second && gasStand >= 1)
        {
            ThirdMission_2();
        }

        if (third && gasTank >= 3)
        {
            if (evoNum >= 2)
            {
                FiveMission_2();
            }
            else
            {
                FourthMission_2();
            }
        }

        if (evoNum >= 2 && fourth)
        {
            timer_2_1 += Time.deltaTime;
        }

        if (five)
        {
            tipsTimer += Time.deltaTime;
        }

        if (tipsTimer >= 180)
        {
            buildTips.SetActive(true);
        }

        if (fourth && timer_2_1 >= 1.0f)
        {
            FiveMission_2();
        }

        if (five && dis <= 50)
        {
            FinalMission_2();
        }

        if (achieve >= 99)
        {
            SecondMission_2();
        }
    }

    public void FirstMission_2()
    {
        first = true;
        missionSlide.Play();
        mission.text = splitText[0];
        submis.text = splitText[1];
        exmis.text = splitText[2];
        per.text = "0%";
        load = "first";
    }

    public void SecondMission_2()
    {
        missionSlide.Play();
        mission.text = splitText[3];
        submis.text = splitText[4];
        exmis.text = splitText[5];
        first = false;
        second = true;
        achieve = 0;
        per.text = achieve + "/ 1";
        load = "second";
    }

    public void ThirdMission_2()
    {
        missionSlide.Play();
        mission.text = splitText[6];
        submis.text = splitText[7];
        exmis.text = splitText[8];
        second = false;
        third = true;
        achieve = 0;
        per.text = achieve + "/ 3";
        load = "third";
    }

    public void FourthMission_2()
    {
        missionSlide.Play();
        mission.text = splitText[9];
        submis.text = splitText[10];
        exmis.text = splitText[11];
        third = false;
        fourth = true;
        achieve = 0;
        per.text = "";
        load = "fourth";
    }

    public void FiveMission_2()
    {
        bossIcon.SetActive(true);

        timer_2_1 = 0;
        missionSlide.Play();
        mission.text = splitText[12];
        submis.text = splitText[13];
        exmis.text = splitText[14];
        third = false;
        fourth = false;
        five = true;
        achieve = 0;
        per.text = "";
        load = "five";
    }

    public void FinalMission_2()
    {
        eneBillScr.changeDamageFlg();

        missionSlide.Play();
        mission.text = splitText[15];
        submis.text = splitText[16];
        exmis.text = splitText[17];
        five = false;
        final = true;
        achieve = 0;
        per.text = "";
        load = "final";
    }
}
