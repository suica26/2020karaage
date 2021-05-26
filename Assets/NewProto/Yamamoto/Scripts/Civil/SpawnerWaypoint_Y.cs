using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWaypoint_Y : WayPoint_Y
{
    private WayPointGraph_Y wayPointGraph;
    public GameObject[] civilPrefabs;
    private GameObject civil;
    public float blurScale;
    private GameObject[] route;

    public float routineTimer;
    public float spawnTime;

    void Start()
    {
        wayPointGraph = GameObject.Find("GameAI_Y").GetComponent<WayPointGraph_Y>();
        //ステージを開始したときに市民がいるようにするため
        routineTimer = spawnTime;
    }

    void Update()
    {
        routineTimer += Time.deltaTime;
        if (routineTimer >= spawnTime)
        {
            SpawnCivil();
        }
    }

    public void SpawnCivil()
    {
        routineTimer = 0f;
        civil = Instantiate(civilPrefabs[(Random.Range(0, civilPrefabs.Length))], InstantiatePositionBlur(), Quaternion.identity);
        wayPointGraph.CulDijkstra(this.PointNumber);
        wayPointGraph.GetRoute(route);
        civil.GetComponent<Civil_Y>().RouteSetting(route);
    }

    private Vector3 InstantiatePositionBlur()
    {
        float x = transform.position.x + Random.Range(-blurScale, blurScale);
        float z = transform.position.z + Random.Range(-blurScale, blurScale);
        var instantiatePos = new Vector3(x, transform.position.y, z);
        return instantiatePos;
    }
}
