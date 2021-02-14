﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav_Y : MonoBehaviour
{
    //これは敵に付けてください
    private NavMeshAgent nav;
    private Vector3 targetPos;
    public float eneDis = 20.0f;//追加
    public bool navFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        nav = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = GameObject.Find("Player").transform.position;
        //この下変更＆追加
        if (Vector3.Distance(targetPos, this.transform.position) <= eneDis)
        {
            nav.enabled = true;
            nav.destination = targetPos;
            navFlg = true;
        }
        else
        {
            nav.enabled = false;
            navFlg = false;
        }
    }
}
