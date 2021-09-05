using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3_M : Missions_M
{
    public string onLoad_s3;
    public GameObject eneBill;
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
            SecondMission_3();
        }

        if (!eneBill)
        {
            PlayerPrefs.SetString("storyClear", "clear");
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
        eneBillScr.changeDamageFlg();

        missionSlide.Play();
        mission.text = splitText[3];
        submis.text = splitText[4];
        exmis.text = splitText[5];
        per.text = "0%";
        load = "second";
        first = false;
    }
}
