using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard_Y : MonoBehaviour
{
    public int shardDamage;
    private bool gaveDamage = false;

    private void OnCollisionEnter(Collision other)
    {
        if (!gaveDamage)
        {
            var objScr = other.gameObject.GetComponent<ObjectStateManagement_Y>();
            if (objScr != null && !objScr.notDamage)
            {
                objScr.HP -= shardDamage;
                objScr.SetSkillID(0);
                objScr.LivingCheck();
                gaveDamage = true;
            }
        }
    }
}
