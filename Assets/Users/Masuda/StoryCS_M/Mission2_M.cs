using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission2_M : Missions_M
{
    public int gasStand, gasTank, blastCount;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        misBox.SetActive(true);
        missionSlide.Play();
        mission.text = splitText[0];
        submis.text = splitText[1];
        exmis.text = splitText[2];
        count.text = "1";
        per.text = "0%";
        first = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 comPos = company.transform.position;
        float dis = Vector3.Distance(playerPos, comPos);

        if (bigNum >= bigBorder4 && first == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            first = false;
            second = true;
            achieve = 0;
            per.text = achieve + "/ 1";
        }
        else if (bigNum >= bigBorder3 && smallNum >= smallBorder1 && first == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            first = false;
            second = true;
            achieve = 0;
            per.text = achieve + "/ 1";
        }
        else if (bigNum >= bigBorder2 && smallNum >= smallBorder2 && first == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            first = false;
            second = true;
            achieve = 0;
            per.text = achieve + "/ 1";
        }
        else if (bigNum >= bigBorder1 && smallNum >= smallBorder3 && first == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            first = false;
            second = true;
            achieve = 0;
            per.text = achieve + "/ 1";
        }

        if (second && gasStand >= 1)
        {
            missionSlide.Play();
            mission.text = splitText[6];
            submis.text = splitText[7];
            exmis.text = splitText[8];
            count.text = "3";
            second = false;
            third = true;
            achieve = 0;
            per.text = achieve + "/ 1";
        }

        if (third && gasTank >= 3)
        {
            missionSlide.Play();
            mission.text = splitText[9];
            submis.text = splitText[10];
            exmis.text = splitText[11];
            count.text = "4";
            third = false;
            fourth = true;
            achieve = 0;
            per.text = achieve + "/ 3";
        }

        if (fourth)
        {
            tipsTimer += Time.deltaTime;
        }

        if (tipsTimer >= 180)
        {
            buildTips.SetActive(true);
        }

        if (fourth && dis <= 30)
        {
            eneBillScr.changeDamageFlg();

            missionSlide.Play();
            mission.text = splitText[12];
            submis.text = splitText[13];
            exmis.text = splitText[14];
            count.text = "5";
            fourth = false;
            final = true;
            achieve = 0;
            per.text = "";
        }
    }

    public void BlastBreak()
    {
        /*switch(eneBillScr.hitSkilID)
        {
            case 3: blastCount++; break;
        }*/
        if (eneBillScr.hitSkilID == 3)
        {
            blastCount++;
        }
    }
}
