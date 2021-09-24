using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarCount_M : MonoBehaviour
{
    private Mission1_M m1m;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        m1m = player.GetComponent<Mission1_M>();
    }

    private void OnDestroy()
    {
        if (m1m != null && m1m.six)
        {
            m1m.CarDestroy();
        }
    }
}
