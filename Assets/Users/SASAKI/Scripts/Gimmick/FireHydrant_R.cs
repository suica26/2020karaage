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
        scrObjManage = this.GetComponent<ObjectStateManagement_Y>();
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
            s1mm.hydrant += 1;
        }
    }
}
