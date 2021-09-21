using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BossBuilding_Y : ObjectStateManagement_Y
{
    protected override void Death()
    {
        base.Death();
        var saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager_Y>();
        saveManager.SaveClearFlg(1);
    }
}
