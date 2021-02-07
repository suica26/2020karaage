using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBreak_Y : MonoBehaviour
{
    private int chikenFormNum = 0;
    public int chainStartStage = 1;
    public int chainDamage = 100;
    private BulidngBreak_Y breakScript = null;

    public Vector3 expStartPos; //爆発の発生地点
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Chain(GameObject obj)
    {
        obj.tag = "Chain";
        if (obj.GetComponent<BoxCollider>() == null) obj.AddComponent<BoxCollider>();
        obj.GetComponent<BoxCollider>().isTrigger = true;
    }
}
