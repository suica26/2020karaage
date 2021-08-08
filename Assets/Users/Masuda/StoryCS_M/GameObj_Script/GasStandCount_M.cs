using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStandCount_M : MonoBehaviour
{
    private Mission2_M m2m;
    public GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
        m2m = player.GetComponent<Mission2_M>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.gameObject && m2m.second)
        {
            m2m.gasStand += 1;
            m2m.achieve += 1;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            m2m.gasStand += 1;
            m2m.achieve += 1;
        }
    }
}
