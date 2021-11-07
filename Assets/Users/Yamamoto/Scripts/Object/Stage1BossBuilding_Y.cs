using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1BossBuilding_Y : ObjectStateManagement_Y
{
    protected override void Start()
    {
        base.Start();
        if (ScoreAttack_Y.gameMode == mode.ScoreAttack) notDamage = false;
    }

    protected override void Death()
    {
        base.Death();
        var saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager_Y>();
        saveManager.SaveClearFlg(1);
    }
}
