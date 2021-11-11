﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission1_M : Missions_M
{
    public GameObject hip, shop, evoPanel;
    public int manhole, hydrant, car, shard, evoCount;
    public bool hipStamp = false, evolution = false;
    public float timer2, timer3;
    public GameObject bossIcon;
    public bool mobileMode;

    public override void Start()
    {
        base.Start();
        hip.SetActive(false);
        evoPanel.SetActive(false);
        misBox.SetActive(false);
        GameObject findCanvas = GameObject.Find("Canvas");
        scrParame = findCanvas.GetComponent<Parameters_R>();
        first = true;

        if (ScoreAttack_Y.gameMode == mode.ScoreAttack)
        {
            evolution = true;
        }
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        evoCount = scrParame.ep;
        evoNum = scrEvoChi.nowEvoNum;

        if (!shop && first)
        {
            //建物にあいさつ
            FirstMission();
        }

        if (second)
        {
            //小物シュート
            if ((bigNum >= bigBorder4) ||
                (bigNum >= bigBorder3 && smallNum >= smallBorder1) ||
                (bigNum >= bigBorder2 && smallNum >= smallBorder2) ||
                (bigNum >= bigBorder1 && smallNum >= smallBorder3))
                SecondMission();
        }

        if (shard >= 3 && third)
        {
            //消火栓
            ThirdMission();
        }

        if (hydrant >= 3 && fourth)
        {
            //マンホール
            FourthMission();
        }

        if (five && !hipStamp)
        {
            timer += Time.unscaledDeltaTime / 2;
        }

        if (timer >= 3.0f)
        {
            Time.timeScale = 0f;
            hip.SetActive(true);
            Cursor.visible = true;
        }

        if (hipStamp)
        {
            timer = 0;
        }

        if (manhole >= 3 && five)
        {
            //車
            FiveMission();
        }

        if (car >= 3 && six)
        {
            if (evoNum >= 1)
            {
                //アジト探し
                SevenMission();
                shibuLight.SetActive(true);
            }

            else
            {
                //進化
                SixMission();
            }
        }

        if (evoNum >= 1 && seven)
        {
            timer3 += Time.deltaTime;
            bossIcon.SetActive(true);
            //一時的に、アジトを探すミッションが発動した時点でミニマップアイコンが見えるように挙動修正 山本
        }

        if (timer3 >= 1.0f && seven)
        {
            //アジト探し
            SevenMission();
            shibuLight.SetActive(true);
        }

        if (final)
        {
            tipsTimer += Time.deltaTime;
        }

        if (company != null)
        {
            Vector3 comPos = company.transform.position;
            float dis = Vector3.Distance(playerPos, comPos);
            if (dis <= 40 && final)
            {
                //破壊
                FinalMission();
            }
        }

        if (tipsTimer >= 30 && !tip)
        {
            if (playLanguage == "English")
            {
                tips.text = "The hideout is shining golden";
            }
            else if (playLanguage == "Japanese")
            {
                tips.text = "アジトは金色に輝いているみたい...？？";
            }
            tip = true;
            tipsChicken.SetActive(true);
        }

        else if (tipsTimer >= 60 && tip)
        {
            if (playLanguage == "English")
            {
                tips.text = "Let's look around using a stream of water！";
            }
            else if (playLanguage == "Japanese")
            {
                tips.text = "消火栓やマンホールを使って\n見渡してみよう...！";
            }
            tipsTimer = 0;
        }

        if (achieve >= 99)
        {
            SecondMission();
        }

        if (evoCount >= scrParame.evo1 * 0.5 && !evolution)
        {
            timer2 += Time.unscaledDeltaTime;
        }
        if (timer2 >= 0.1f)
        {
            evoPanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
        if (!evoPanel)
        {
            timer2 = 0;
        }

    }

    public void OnClick1()
    {
        evoPanel.SetActive(false);
        timer2 = 0;
        Time.timeScale = 1f;
        evolution = true;
        Cursor.visible = false;
    }

    public void OnClick2()
    {
        hip.SetActive(false);
        timer = 0;
        Time.timeScale = 1f;
        hipStamp = true;
        Cursor.visible = false;
    }

    void FirstMission()
    {
        misBox.SetActive(true);
        missionSlide.Play();
        mission.text = splitText[0];
        submis.text = splitText[1];
        exmis.text = splitText[2];
        first = false;
        second = true;
        per.text = "0%";
    }

    void SecondMission()
    {
        missionSlide.Play();
        mission.text = splitText[3];
        submis.text = splitText[4];
        exmis.text = splitText[5];
        second = false;
        third = true;
        achieve = 0;
        per.text = achieve + "/ 3";
    }

    void ThirdMission()
    {
        missionSlide.Play();
        mission.text = splitText[6];
        submis.text = splitText[7];
        exmis.text = splitText[8];
        third = false;
        fourth = true;
        achieve = 0;
        per.text = achieve + "/ 3";
    }

    void FourthMission()
    {
        fourth = false;
        five = true;
        missionSlide.Play();
        mission.text = splitText[9];
        submis.text = splitText[10];
        exmis.text = splitText[11];
        achieve = 0;
        per.text = achieve + "/ 3";
    }

    void FiveMission()
    {
        five = false;
        six = true;
        missionSlide.Play();
        mission.text = splitText[12];
        submis.text = splitText[13];
        exmis.text = splitText[14];
        achieve = 0;
        per.text = achieve + "/ 3";
    }

    void SixMission()
    {
        six = false;
        seven = true;
        missionSlide.Play();
        mission.text = splitText[15];
        submis.text = splitText[16];
        exmis.text = splitText[17];
        per.text = "";
    }

    void SevenMission()
    {
        seven = false;
        six = false;
        final = true;
        missionSlide.Play();
        mission.text = splitText[18];
        submis.text = splitText[19];
        exmis.text = splitText[20];
        per.text = "";
    }

    void FinalMission()
    {
        eneBillScr.changeDamageFlg();

        missionSlide.Play();
        mission.text = splitText[21];
        submis.text = splitText[22];
        exmis.text = splitText[23];
        final = false;
    }

    public void CarDestroy()
    {
        if (six)
        {
            car += 1;
            achieve += 1;
            per.text = achieve + " / 3";
        }
    }

    public void ShardAttack()
    {
        if (third)
        {
            shard += 1;
            achieve += 1;
            per.text = achieve + " / 3";
        }
    }

    public override void SmallNumberPlus()
    {
        if (second)
        {
            smallNum++;
            achieve += 4;
            per.text = achieve + "%";
        }
    }

    public override void BigNumberPlus()
    {
        if (second)
        {
            bigNum++;
            achieve += 20;
            per.text = achieve + "%";
        }
    }
}
