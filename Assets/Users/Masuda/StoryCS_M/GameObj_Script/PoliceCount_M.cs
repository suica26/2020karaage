using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCount_M : MonoBehaviour
{
    private Police_Y police;
    private GameObject player;
    private Mission2_M m2m;
    private bool safety;

    void Start()
    {
        player = GameObject.Find("Player");
        m2m = player.GetComponent<Mission2_M>();
        police = this.GetComponent<Police_Y>();
    }

    // Update is called once per frame
    void Update()
    {
        if (police.HP <= 0 && !safety)
        {
            m2m.EnemyBreak();
            safety = true;
        }
    }
}
