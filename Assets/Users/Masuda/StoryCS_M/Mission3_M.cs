using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission3_M : Missions_M
{
    public string onLoad_s3;
    public GameObject eneBill;
    public float evoTimer;
    public GameObject bossIcon, perSub;
    public Text per2;
    public int achieve2;
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

        //支部の段階
        if (third)
        {
            if (achieve >= 25 && achieve2 >= 10)
            {
                FourthMission_3();
            }
        }
        if (five)
        {
            if (achieve >= 25 && achieve2 >= 10)
            {
                SixMission_3();
            }
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
        per.text = "0 / 25";//シェフ
        per2.text = "0 / 10";//警官
        second = false;
        third = true;
    }

    public void FourthMission_3()
    {
        //支部攻撃2
        missionSlide.Play();
        submis.text = splitText[8];
        exmis.text = splitText[9];
        per.text = "";
        per2.text = "";
        third = false;
        fourth = true;
    }

    public void FiveMission_3()
    {
        //敵破壊2
        missionSlide.Play();
        submis.text = splitText[10];
        exmis.text = splitText[11];
        achieve = 0;
        achieve2 = 0;
        per.text = "0 / 25";//警官
        per2.text = "0 / 10";//戦車
        fourth = false;
        five = true;
    }

    public void SixMission_3()
    {
        //支部撃破
        missionSlide.Play();
        submis.text = splitText[12];
        exmis.text = splitText[13];
        per.text = "";
        per2.text = "";
        five = false;
        six = true;
    }

    public void EnemyBreak_Chef()
    {
        if (third || five)
        {
            achieve += 1;
            if (achieve >= 25)
            {
                achieve = 25;
            }
            per.text = achieve + " / 25";
        }
    }

    public void EnemyBreak_Police()
    {
        if (third)
        {
            achieve2 += 1;
            if (achieve2 >= 10)
            {
                achieve2 = 10;
            }
            per2.text = achieve2 + " / 10";
        }
        else if (five)
        {
            achieve += 1;
            if (achieve >= 25)
            {
                achieve = 25;
            }
            per.text = achieve + " / 25";
        }
    }

    public void EnemyBreak_Tank()
    {
        if (third || five)
        {
            achieve2 += 1;
            if (achieve2 >= 10)
            {
                achieve2 = 10;
            }
            per2.text = achieve2 + " / 10";
        }
    }
}
