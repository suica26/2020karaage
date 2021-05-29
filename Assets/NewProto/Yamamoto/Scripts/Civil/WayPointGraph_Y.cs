﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointGraph_Y : MonoBehaviour
{
    public GameObject wayPoints;
    private GameObject[] wayPointsArray;
    private WayPoint_Y[] wpScripts;
    private List<int> endPointNumbers;
    private List<SpawnerWaypoint_Y> scrSpawners;
    private int NOC = 0;   //計算回数
    public GameObject[] route;

    [SerializeField] private int civilMaxNum;
    public int civilNum;
    public float routinTimer;
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        //コストマップ情報を構築
        wayPointsArray = new GameObject[wayPoints.transform.childCount];
        wpScripts = new WayPoint_Y[wayPointsArray.Length];
        for (int i = 0; i < wpScripts.Length; i++)
        {
            wayPointsArray[i] = wayPoints.transform.GetChild(i).gameObject;
            wpScripts[i] = wayPointsArray[i].GetComponent<WayPoint_Y>();
            wpScripts[i].SetPointNum(i);
        }

        //リスト初期化
        endPointNumbers = new List<int>();
        scrSpawners = new List<SpawnerWaypoint_Y>();

        //隣接点の情報を取得
        foreach (var wps in wpScripts)
        {
            //隣接点情報を各WayPointにセット
            wps.SetNeiNum();
            //終着点候補をリストに追加していく
            if (wps.endPointFlg) endPointNumbers.Add(wps.PointNumber);
            //スポーン候補をリストに追加していく
            if (wps.spawnerPointFlg) scrSpawners.Add(wayPointsArray[wps.PointNumber].GetComponent<SpawnerWaypoint_Y>());
        }
    }

    void Update()
    {
        //一度に存在する市民の数を制御
        if (civilNum < civilMaxNum) routinTimer += Time.deltaTime;
        //スポーン処理
        if (routinTimer >= spawnTime) Spawn();
    }

    private void Spawn()
    {
        routinTimer = 0f;
        civilNum++;
        scrSpawners[Random.Range(0, scrSpawners.Count)].SpawnCivil();
    }

    public void CulDijkstra(int startPoint)
    {
        int endPoint = endPointNumbers[Random.Range(0, endPointNumbers.Count)];
        while (endPoint == startPoint) endPoint = endPointNumbers[Random.Range(0, endPointNumbers.Count)];

        bool finishFlg = false;
        var nextList = new List<int>();
        int[] checkPoints;

        //開始地点(スポーン地点)の設定
        wpScripts[startPoint].ChangeRouteNumber(0, -100);
        checkPoints = wpScripts[startPoint].NeiNums;

        while (finishFlg)
        {
            NOC++;
            foreach (var points in checkPoints)
            {
                wpScripts[points].ChangeRouteNumber(NOC, points);
                if (points == endPoint)
                {
                    finishFlg = true;
                    break;
                }
                foreach (var neighbors in wpScripts[points].NeiNums)
                {
                    if (wpScripts[neighbors].RouteNumber < 0)
                    {
                        nextList.Add(neighbors);
                    }
                }
            }
            checkPoints = nextList.ToArray();
        }
        CreateRoute(endPoint);
    }

    public void CreateRoute(int endPoint)
    {
        bool finish = false;
        var routeList = new List<GameObject>();
        int before = endPoint;

        while (finish)
        {
            if (wpScripts[before].beforePoint == -100)
            {
                finish = true;
            }
            else
            {
                routeList.Add(wayPointsArray[before]);
                before = wpScripts[before].beforePoint;
            }
        }

        routeList.Reverse();
        route = routeList.ToArray();
    }

    public void GetRoute(GameObject[] newRoute)
    {
        newRoute = route;
    }

    public void ResetDijkstraMap()
    {
        foreach (var scr in wpScripts)
        {
            scr.ChangeRouteNumber(-1, -1);
            NOC = -1;
        }
    }

    public void CivilNumDecrease()
    {
        civilNum--;
    }
}
