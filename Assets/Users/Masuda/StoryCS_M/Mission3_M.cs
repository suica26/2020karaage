using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission3_M : Missions_M
{
    public string onLoad_s3;
    public GameObject eneBill,shibuLight2;
    public GameObject[] shibuEff;
    public float evoTimer;
    public GameObject bossIcon, perSub;
    public Text per2;
    public int achieve2, achieve3;
    public override void Start()
    {
        base.Start();
        shibuLight2 = GameObject.Find("Light_pillar (1)");
        shibuEff[0] = GameObject.Find("EneBuilding_eff 1");
        shibuEff[1] = GameObject.Find("EneBuilding_eff 1 (1)");
        shibuEff[2] = GameObject.Find("EneBuilding_eff 1 (2)");
        shibuEff[3] = GameObject.Find("EneBuilding_eff 1 (3)");
        shibuEff[4] = GameObject.Find("EneBuilding_eff 1 (4)");
        shibuEff[5] = GameObject.Find("EneBuilding_eff 1 (5)");
        shibuEff[6] = GameObject.Find("EneBuilding_eff 1 (6)");
        shibuLight2.SetActive(false);
        for (int i = 0; i < 7; i++)
        {
            shibuEff[i].SetActive(false);
        }
        misBox.SetActive(true);
        Time.timeScale = 1;
        if (ScoreAttack_Y.gameMode == mode.ScoreAttack)
        {
            another = true;
        }
        onLoad_s3 = PlayerPrefs.GetString(scrParame.saveStage, "");
        switch (onLoad_s3)
        {
            case "first": FirstMission_3(); break;
            case "second": SecondMission_3(); break;
            case "another": return;
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

        /*if (!eneBill)
        {
            PlayerPrefs.SetString("storyClear", "clear");
        }*/

        if (first && evoTimer >= 1.0f)
        {
            SecondMission_3();
            shibuLight.SetActive(true);
            shibuLight2.SetActive(true);
            for (int i = 0; i < 7; i++)
            {
                shibuEff[i].SetActive(true);
            }
        }

        if (company == null)//破壊時光る柱を消す
        {
            shibuLight.SetActive(false);
            shibuLight2.SetActive(false);
            for (int i = 0; i < 7; i++)
            {
                shibuEff[i].SetActive(false);
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
        //per.text = "0 / 25";//シェフ
        //per2.text = "0 / 10";//警官
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
        //per.text = "0 / 25";//警官
        //per2.text = "0 / 10";//戦車
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
            /*if (achieve >= 25)
            {
                achieve = 25;
            }
            per.text = achieve + " / 25";*/
        }
    }

    public void EnemyBreak_Police()
    {
        if (third || five)
        {
            achieve2 += 1;
            /*if (achieve2 >= 10)
            {
                achieve2 = 10;
            }
            per2.text = achieve2 + " / 10";*/
        }
    }

    public void EnemyBreak_Tank()
    {
        if (third || five)
        {
            achieve3 += 1;
            /*if (achieve2 >= 10)
            {
                achieve2 = 10;
            }
            per2.text = achieve2 + " / 10";*/
        }
    }
}
