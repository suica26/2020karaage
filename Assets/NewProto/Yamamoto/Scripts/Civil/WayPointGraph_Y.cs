using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointGraph_Y : MonoBehaviour
{
    public GameObject wayPoints;
    private GameObject[] wayPointsArray;
    private WayPoint_Y[] wpScripts;
    private List<int> endPointNumbers;
    private List<SpawnerWaypoint_Y> scrSpawners;
    private int NOC;   //計算回数
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
            //各WayPointにnumberを割り振り
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

        ResetDijkstraMap();
    }

    private void Update()
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
        //セットしてあるPrefabの中から、Spawnする市民をランダムに選択
        int randomNum = Random.Range(0, scrSpawners.Count);
        CulDijkstra(scrSpawners[randomNum].PointNumber);
        scrSpawners[randomNum].SpawnCivil();
    }

    public void CulDijkstra(int startPoint)
    {
        int endPoint;
        do
        {
            endPoint = endPointNumbers[Random.Range(0, endPointNumbers.Count)];
        } while (endPoint == startPoint);

        var nextList = new List<int>();
        var nextNumList = new List<int>();
        int[] checkPoints;
        bool finishFlg = false;

        //開始地点(スポーン地点)の設定
        wpScripts[startPoint].ChangeRouteNumber(0, -100);
        checkPoints = wpScripts[startPoint].NeiNums;

        //beforeポイントの記録用配列とリストの宣言＆初期化
        for (int i = 0; i < checkPoints.Length; i++)
        {
            nextNumList.Add(startPoint);
        }
        //beforePoint配列を次のものにして、リストのほうは初期化
        int[] beforeNums = nextNumList.ToArray();
        nextNumList = new List<int>();

        while (!finishFlg)
        {
            NOC++;
            Debug.Log("NOC = " + NOC);
            for (int i = 0; i < checkPoints.Length; i++)
            {
                //今回分の探索処理
                wpScripts[checkPoints[i]].ChangeRouteNumber(NOC, beforeNums[i]);
                if (checkPoints[i] == endPoint)
                {
                    finishFlg = true;
                }
                //次回探索する分を作成
                foreach (int neighbors in wpScripts[checkPoints[i]].NeiNums)
                {
                    //すでに計算済みの物は再計算しないように
                    if (wpScripts[neighbors].RouteNumber < 0)
                    {
                        nextList.Add(neighbors);
                        nextNumList.Add(checkPoints[i]);
                    }
                }
            }

            //配列とリストを次のものに更新
            beforeNums = nextNumList.ToArray();
            nextNumList = new List<int>();
            checkPoints = nextList.ToArray();
            nextList = new List<int>();

            if (NOC > 200)
            {
                Debug.Log("Infinite Loop Avoided!");
                break;
            }
        }

        finishFlg = false;
        CreateRoute(endPoint);
    }

    public void CreateRoute(int endPoint)
    {
        bool finish = false;
        var routeList = new List<GameObject>();
        int before = endPoint;

        while (!finish)
        {
            if (wpScripts[before].BeforePoint == -100)
            {
                finish = true;
            }
            else
            {
                routeList.Add(wayPointsArray[before]);
                before = wpScripts[before].BeforePoint;
            }
        }

        routeList.Reverse();
        route = routeList.ToArray();
    }

    public void ResetDijkstraMap()
    {
        foreach (var scr in wpScripts)
        {
            scr.ChangeRouteNumber(-1, -1);
            NOC = 0;
        }
    }

    public void CivilNumDecrease()
    {
        civilNum--;
    }
}
