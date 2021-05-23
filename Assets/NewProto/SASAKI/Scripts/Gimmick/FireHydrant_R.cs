using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant_R : BlowerGimmickBase
{
    private bool makeHydrant = false;
    private ObjectStateManagement_Y scrObjManage;

    void Start()
    {
        scrObjManage = this.GetComponent<ObjectStateManagement_Y>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!makeHydrant && scrObjManage.HP <= 0)
        {
            makeHydrant = true;
            InstanceObject();
            InstanceEffect();
        }
    }
}
