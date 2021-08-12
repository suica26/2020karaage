﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDamage_Y : MonoBehaviour
{
    public int damage;
    private Parameters_R param;

    void Start()
    {
        param = GameObject.Find("Canvas").GetComponent<Parameters_R>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            param.HPManager(damage);
        }
    }
}