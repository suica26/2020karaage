using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrantCount_M : MonoBehaviour
{
    private Mission1_M m1m;
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        m1m = player.GetComponent<Mission1_M>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m1m != null)
        {
            if (m1m.third && !this)
            {
                m1m.hydrant += 1;
                m1m.achieve += 1;
                m1m.per.text = m1m.achieve + "/ 3";
            }

            if (!this)
            {
                m1m.hydrant += 1;
                m1m.achieve += 1;
                m1m.per.text = m1m.achieve + "/ 3";
            }
        }
    }
}
