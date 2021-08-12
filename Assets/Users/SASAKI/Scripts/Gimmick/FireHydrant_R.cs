using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant_R : BlowerGimmickBase
{
    private bool makeHydrant = false;
    private ObjectStateManagement_Y scrObjManage;
    //M
    //private Stage1_Mission_M s1mm;
    private Mission1_M m1m;
    public GameObject player;
    public bool hyd;

    void Start()
    {
        scrObjManage = GetComponent<ObjectStateManagement_Y>();
        //M
        player = GameObject.Find("Player");
        //s1mm = player.GetComponent<Stage1_Mission_M>();
        m1m = player.GetComponent<Mission1_M>();
        if (this.gameObject.tag == "Small")
        {
            hyd = true;
        }
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
            if (m1m.third && hyd)
            {
                m1m.hydrant += 1;
                m1m.achieve += 1;
                m1m.per.text = m1m.achieve + "/ 3";
            }
        }
    }
}
