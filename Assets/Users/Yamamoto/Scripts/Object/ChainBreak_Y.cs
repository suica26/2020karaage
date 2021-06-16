using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBreak_Y : MonoBehaviour
{
    public int chainDamage = 100;
    public Vector3 expStartPos; //爆発の発生地点

    public void Chain(GameObject obj)
    {
        obj.tag = "Chain";
        if (obj.GetComponent<BoxCollider>() == null) obj.AddComponent<BoxCollider>();
        obj.GetComponent<BoxCollider>().isTrigger = true;
    }
}
