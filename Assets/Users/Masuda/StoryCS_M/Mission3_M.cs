using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3_M : Missions_M
{
    public string onLoad;
    public GameObject eneBill;
    public override void Start()
    {
        base.Start();
        misBox.SetActive(true);
        Time.timeScale = 1;
        onLoad = PlayerPrefs.GetString(scrParame.saveStage, "");
        Debug.Log(onLoad);//後消し
        switch (onLoad)
        {
            case "first": FirstMission_3(); break;
            case "second": SecondMission_3(); break;
            default: FirstMission_3(); break;
        }
        PlayerPrefs.DeleteKey(onLoad);
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
        count.text = "1";
        per.text = "0%";
        load = "first";
    }

    public void SecondMission_3()
    {
        missionSlide.Play();
        mission.text = splitText[3];
        submis.text = splitText[4];
        exmis.text = splitText[5];
        count.text = "1";
        per.text = "0%";
        load = "second";
        first = false;
    }
}
