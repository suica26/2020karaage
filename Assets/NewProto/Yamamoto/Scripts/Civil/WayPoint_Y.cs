using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint_Y : MonoBehaviour
{
    public GameObject[] neighbor;
    public int[] NeiNums { get; private set; }
    public int RouteNumber { get; private set; }
    public int PointNumber { get; private set; }
    public int beforePoint { get; private set; }
    public bool endPointFlg;
    public bool spawnerPointFlg;

    private void Start()
    {
        RouteNumber = -1;
        beforePoint = -1;
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
        beforePoint = before;
    }
    public void SetPointNum(int num)
    {
        PointNumber = num;
    }
}
