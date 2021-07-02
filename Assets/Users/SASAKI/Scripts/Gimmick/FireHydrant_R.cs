using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant_R : BlowerGimmickBase
{
    private bool makeHydrant = false;
    private ObjectStateManagement_Y scrObjManage;
    //M
    private Stage1_Mission_M s1mm;
    public GameObject player;

    void Start()
    {
        scrObjManage = GetComponent<ObjectStateManagement_Y>();
        //M
        player = GameObject.Find("Player");
        s1mm = player.GetComponent<Stage1_Mission_M>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!makeHydrant && scrObjManage.HP <= 0)
        {
            makeHydrant = true;
            InstanceObject();
            InstanceEffect();
            //M
            if (s1mm.third)
            {
                s1mm.hydrant += 1;
                s1mm.achieve += 1;
                s1mm.per.text = s1mm.achieve + "/ 3";
            }
        }
    }
}
