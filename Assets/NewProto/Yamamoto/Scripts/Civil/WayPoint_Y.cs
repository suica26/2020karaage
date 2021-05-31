﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint_Y : MonoBehaviour
{
    public GameObject[] neighbor;
    public int[] NeiNums;
    public int RouteNumber { get; private set; }
    public int PointNumber;
    public int BeforePoint;
    public bool endPointFlg;
    public bool spawnerPointFlg;

    private void Start()
    {
        RouteNumber = -1;
        BeforePoint = -1;
    }

    public void SetNeiNum()
    {
        NeiNums = new int[neighbor.Length];
        for (int i = 0; i < neighbor.Length; i++)
        {
            NeiNums[i] = neighbor[i].GetComponent<WayPoint_Y>().PointNumber;
        }
    }

    public void ChangeRouteNumber(int num, int before)
    {
        RouteNumber = num;
        BeforePoint = before;
    }
    public void SetPointNum(int num)
    {
        PointNumber = num;
    }
}
