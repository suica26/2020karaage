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

    void Start()
    {
        s1Mis = GetComponent<Stage1_Mission_M>();
        osManage = GetComponent<ObjectStateManagement_Y>();
        buildHP = osManage.HP;
        buildLife = osManage.notLive;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //テスト
        if (Input.GetKeyDown(KeyCode.T))
        {
            player.GetComponent<Stage1_Mission_M>().BigNumberPlus();
        }

        if (buildHP <= 0 && !buildLife)
        {
            if (this.gameObject.tag == "Small")
            {
                player.GetComponent<Stage1_Mission_M>().SmallNumberPlus();
            }
            else if (this.gameObject.tag == "Big")
            {
                player.GetComponent<Stage1_Mission_M>().BigNumberPlus();
            }
        }
    }
}
