using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStandCount_M : MonoBehaviour
{
    private Mission2_M m2m;
    private ObjectStateManagement_Y osmY;
    public GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
        m2m = player.GetComponent<Mission2_M>();
        osmY = this.gameObject.GetComponent<ObjectStateManagement_Y>();
    }

    // Update is called once per frame
    void Update()
    {
        if (osmY.HP <= 0 && m2m.second)
        {
            m2m.gasStand += 1;
            m2m.achieve += 1;
        }
    }
}
