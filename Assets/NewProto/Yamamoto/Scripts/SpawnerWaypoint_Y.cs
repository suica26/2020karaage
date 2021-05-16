using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWaypoint_Y : MonoBehaviour
{
    [Header("設定したルートの数と同じにしてください")]
    [Range(1, 5)] public int maxRouteNum;
    public GameObject[] route1;
    public GameObject[] route2;
    public GameObject[] route3;
    public GameObject[] route4;
    public GameObject[] route5;
    private GameObject[][] routes;
    public GameObject[] civilPrefabs;
    private GameObject civil;
    public float blurScale;

    [SerializeField] private float routineTimer;
    public float spawnTime;

    void Start()
    {
        routes = new GameObject[][] { route1, route2, route3, route4, route5 };
        //ステージを開始したときに市民がいるようにするため
        routineTimer = spawnTime;
    }

    void Update()
    {
        routineTimer += Time.deltaTime;
        if (routineTimer > spawnTime)
        {
            SpawnCivil();
            routineTimer = 0f;
        }
    }

    public void SpawnCivil()
    {
        civil = Instantiate(civilPrefabs[(Random.Range(0, civilPrefabs.Length))], InstantiatePositionBlur(), Quaternion.identity);
        civil.GetComponent<Civil_Y>().RouteSetting(routes[Random.Range(0, maxRouteNum)]);
    }

    private Vector3 InstantiatePositionBlur()
    {
        float x = transform.position.x + Random.Range(-blurScale, blurScale);
        float z = transform.position.z + Random.Range(-blurScale, blurScale);
        var instantiatePos = new Vector3(x, transform.position.y, z);
        return instantiatePos;
    }
}
