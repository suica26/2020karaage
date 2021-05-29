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
    private Civil_Y scrCivil;

    private void Start()
    {
        wayPointGraph = GameObject.Find("GameAI_Y").GetComponent<WayPointGraph_Y>();
    }

    public void SpawnCivil()
    {
        civil = Instantiate(civilPrefabs[(Random.Range(0, civilPrefabs.Length))], InstantiatePositionBlur(), Quaternion.identity);
        wayPointGraph.CulDijkstra(this.PointNumber);
        wayPointGraph.GetRoute(route);

        scrCivil = civil.GetComponent<Civil_Y>();
        scrCivil.RouteSetting(route);   //ルート情報付与
        scrCivil.SetWayPointGraph(wayPointGraph);   //コストマップ情報付与
    }

    private Vector3 InstantiatePositionBlur()
    {
        float x = transform.position.x + Random.Range(-blurScale, blurScale);
        float z = transform.position.z + Random.Range(-blurScale, blurScale);
        var instantiatePos = new Vector3(x, transform.position.y, z);
        return instantiatePos;
    }
}
