using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_BuildingBreak_M : MonoBehaviour
{
    [SerializeField] private int buildHP;
    [SerializeField] private bool buildLife;
    private Stage1_Mission_M s1Mis;
    private ObjectStateManagement_Y osManage;
    private GameObject player;

    //山本加筆 Update内のGetComponent処理回避
    private Stage1_Mission_M scrStage1Mission;

    void Start()
    {
        s1Mis = GetComponent<Stage1_Mission_M>();
        osManage = GetComponent<ObjectStateManagement_Y>();
        buildHP = osManage.HP;
        buildLife = osManage.notLive;
        player = GameObject.Find("Player");
        scrStage1Mission = player.GetComponent<Stage1_Mission_M>();
    }

    // Update is called once per frame
    void Update()
    {
        //テスト
        if (Input.GetKeyDown(KeyCode.T))
        {
            //山本加筆　元:player.GetComponent<Stage1_Mission_M>().BigNumberPlus();
            scrStage1Mission.BigNumberPlus();
        }

        if (buildHP <= 0 && !buildLife)
        {
            if (this.gameObject.tag == "Small")
            {
                //山本加筆　元:player.GetComponent<Stage1_Mission_M>().SmallNumberPlus();
                scrStage1Mission.SmallNumberPlus();
            }
            else if (this.gameObject.tag == "Big")
            {
                //山本加筆　元:player.GetComponent<Stage1_Mission_M>().BigNumberPlus();
                scrStage1Mission.BigNumberPlus();
            }
        }
    }
}
