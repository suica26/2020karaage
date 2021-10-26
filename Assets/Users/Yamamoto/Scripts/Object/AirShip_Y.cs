﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Y : ObjectStateManagement_Y
{
    public float flyingHeight;
    public float rotSpeed;
    public float rotTimer;
    public Vector3 rotCenter;
    public float radius;
    private delegate void action();
    private action[] BreakActions;

    protected override void Start()
    {
        base.Start();
        BreakActions = new action[6]{GenerateFood,GenerateChef,GeneratePollice,GenerateTank,GenerateEnemys,GenerateMiniChickens};
    }

    // Update is called once per frame
    void Update()
    {
        if(livingFlg)
        {
            rotTimer += Time.deltaTime + rotSpeed;
            transform.position = rotCenter + new Vector3(Mathf.Cos(rotTimer), flyingHeight, Mathf.Sin(rotTimer)) * radius;
        }
    }

    protected override void Death()
    {
        if (!ChangeToDeath()) return;

        DeathCount();
        
        tag = "Broken";
        gameObject.layer = LayerMask.NameToLayer("BrokenObject");

        //トルク付加
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.useGravity = true;
        var t = new float[3];
        for (int i = 0; i < 3;i++) t[i] = Random.Range(-torque, torque);
        rb.AddTorque(t[0], t[1], t[2]);

        if (breakEffect != null)
            {
                var effect = Instantiate(breakEffect, transform.position, Quaternion.identity);
                Destroy(effect, 1f);
            }

        Delete();
    }

    private void GenerateFood()
    {

    }

    private void GenerateChef()
    {
        
    }

    private void GeneratePollice()
    {
        
    }

    private void GenerateTank()
    {
        
    }

    private void GenerateEnemys()
    {
        
    }

    private void GenerateMiniChickens()
    {
        
    }
}
