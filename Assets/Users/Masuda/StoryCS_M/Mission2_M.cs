using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission2_M : Missions_M
{
    public int gasStand, gasTank, enemyBreak;
    public string onLoad;
    public float timer_2_1;
    public GameObject bossIcon;
    public EnemySpawnController enemySpawnerScr;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        misBox.SetActive(true);
        Time.timeScale = 1;
        onLoad = PlayerPrefs.GetString(scrParame.saveStage, "");
        if (ScoreAttack_Y.gameMode == mode.ScoreAttack)
        {
            another = true;
        }
        switch (onLoad)
        {
            case "first": FirstMission_2(); break;
            case "second": SecondMission_2(); break;
            case "third": ThirdMission_2(); break;
            case "fourth": FourthMission_2(); break;
            case "five": FiveMission_2(); break;
            case "six": SixMission_2(); break;
            case "final": FinalMission_2(); break;
            case "another": return;
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

        if (third && enemyBreak >= 20)
        {
            FourthMission_2();
        }

        if (fourth && gasTank >= 3)
        {
            if (evoNum >= 2)
            {
                SixMission_2();
                shibuLight.SetActive(true);
            }
            else
            {
                FiveMission_2();
            }
        }

        if (evoNum >= 2 && five)
        {
            timer_2_1 += Time.deltaTime;
        }

        if (six)
        {
            tipsTimer += Time.deltaTime;
        }

        if (five && timer_2_1 >= 1.0f)
        {
            SixMission_2();
            shibuLight.SetActive(true);
        }

        if (six && dis <= 50)
        {
            FinalMission_2();
        }

        if (achieve >= 99)
        {
            SecondMission_2();
        }

        if (company == null)//破壊時光る柱を消す
        {
            shibuLight.SetActive(false);
        }
    }

    public void FirstMission_2()
    {
        //ブラスト
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
        //ガソスタ
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
        //敵倒し
        missionSlide.Play();
        mission.text = splitText[6];
        submis.text = splitText[7];
        exmis.text = splitText[8];
        second = false;
        third = true;
        achieve = 0;
        per.text = achieve + "/ 20";
        load = "third";
        enemySpawnerScr.enabled = true;
    }

    public void FourthMission_2()
    {
        //ガスボール
        missionSlide.Play();
        mission.text = splitText[9];
        submis.text = splitText[10];
        exmis.text = splitText[11];
        third = false;
        fourth = true;
        achieve = 0;
        per.text = "";
        achieve = 0;
        per.text = achieve + "/ 3";
        load = "fourth";
    }

    public void FiveMission_2()
    {
        //進化
        missionSlide.Play();
        mission.text = splitText[12];
        submis.text = splitText[13];
        exmis.text = splitText[14];
        fourth = false;
        five = true;
        achieve = 0;
        per.text = "";
        load = "five";
    }

    public void SixMission_2()
    {
        //支部探し
        bossIcon.SetActive(true);

        timer_2_1 = 0;
        missionSlide.Play();
        mission.text = splitText[15];
        submis.text = splitText[16];
        exmis.text = splitText[17];
        fourth = false;
        five = false;
        six = true;
        achieve = 0;
        per.text = "";
        load = "six";
    }
    public void FinalMission_2()
    {
        eneBillScr.changeDamageFlg();

        //支部破壊
        missionSlide.Play();
        mission.text = splitText[18];
        submis.text = splitText[19];
        exmis.text = splitText[20];
        six = false;
        final = true;
        achieve = 0;
        per.text = "";
        load = "final";
    }

    public void EnemyBreak()
    {
        if (third)
        {
            enemyBreak += 1;
            achieve += 1;
            per.text = achieve + "/ 20";
        }
    }

    public override void SmallNumberPlus()
    {
        if (first)
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

    public override void BigNumberPlus()
    {
        if (first)
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
}
