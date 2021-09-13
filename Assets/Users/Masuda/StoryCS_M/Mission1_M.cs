using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission1_M : Missions_M
{
    public GameObject hip, shop, evoPanel;
    public int manhole, hydrant, car, evoCount;
    public bool hipStamp = false, evolution = false;
    public float timer2, timer3;
    public GameObject bossIcon;

    public override void Start()
    {
        base.Start();
        hip.SetActive(false);
        evoPanel.SetActive(false);
        misBox.SetActive(false);
        GameObject findCanvas = GameObject.Find("Canvas");
        scrParame = findCanvas.GetComponent<Parameters_R>();
        first = true;
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 comPos = company.transform.position;
        float dis = Vector3.Distance(playerPos, comPos);
        evoCount = scrParame.ep;
        evoNum = scrEvoChi.nowEvoNum;

        if (!shop && first)
        {
            FirstMission();
        }

        if (bigNum >= bigBorder4 && second == true)
        {
            SecondMission();
        }
        else if (bigNum >= bigBorder3 && smallNum >= smallBorder1 && second == true)
        {
            SecondMission();
        }
        else if (bigNum >= bigBorder2 && smallNum >= smallBorder2 && second == true)
        {
            SecondMission();
        }
        else if (bigNum >= bigBorder1 && smallNum >= smallBorder3 && second == true)
        {
            SecondMission();
        }

        if (hydrant >= 3 && third)
        {
            ThirdMission();
        }

        if (fourth && hipStamp)
        {
            timer += Time.unscaledDeltaTime / 2;
        }

        if (timer >= 3.0f)
        {
            Time.timeScale = 0f;
            hip.SetActive(true);
            Cursor.visible = true;
        }

        if (!hipStamp)
        {
            timer = 0;
        }

        if (manhole >= 3 && fourth)
        {
            if (evoNum >= 1)
            {
                FiveMission();
            }

            else
            {
                SixMission();
            }
        }

        if (evoNum >= 1 && five)
        {
            timer3 += Time.deltaTime;
            bossIcon.SetActive(true);
            //一時的に、アジトを探すミッションが発動した時点でミニマップアイコンが見えるように挙動修正 山本
        }

        if (timer3 >= 1.0f && five)
        {

            FiveMission();
        }

        if (final)
        {
            tipsTimer += Time.deltaTime;
        }

        if (dis <= 40 && final)
        {
            FinalMission();
        }

        if (tipsTimer >= 30 && !tip)
        {
            tips.text = "アジトは金色に輝いているみたい...？？";
            tip = true;
            tipsChicken.SetActive(true);
        }

        else if (tipsTimer >= 60 && tip)
        {
            tips.text = "消火栓やマンホールを使って\n見渡してみよう...！";
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
        Time.timeScale = 1f;
        hip.SetActive(false);
        hipStamp = false;
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
        third = false;
        fourth = true;
        missionSlide.Play();
        mission.text = splitText[6];
        submis.text = splitText[7];
        exmis.text = splitText[8];
        achieve = 0;
        per.text = achieve + "/ 3";
        hipStamp = true;
    }

    void FourthMission()
    {
        
    }

    void FiveMission()
    {
        fourth = false;
        five = false;
        final = true;
        missionSlide.Play();
        mission.text = splitText[12];
        submis.text = splitText[13];
        exmis.text = splitText[14];
        per.text = "";
    }

    void SixMission()
    {
        fourth = false;
        five = true;
        missionSlide.Play();
        mission.text = splitText[9];
        submis.text = splitText[10];
        exmis.text = splitText[11];
        per.text = "";
    }

    void FinalMission()
    {
        eneBillScr.changeDamageFlg();

        missionSlide.Play();
        mission.text = splitText[15];
        submis.text = splitText[16];
        exmis.text = splitText[17];
        final = false;
    }
}
