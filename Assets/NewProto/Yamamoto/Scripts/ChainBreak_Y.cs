using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBreak_Y : MonoBehaviour
{
    private int chikenFormNum = 0;
    public int chainStartStage = 1;
    public int chainDamage = 0;
    private BulidngBreak_Y breakScript = null;
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
        chainDamage = 200;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Object")
        {
            breakScript = other.GetComponent<BulidngBreak_Y>();
            breakScript.HP -= chainDamage;
        }
    }
}
