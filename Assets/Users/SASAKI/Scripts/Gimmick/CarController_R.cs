using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController_R : MonoBehaviour
{
    [SerializeField] private int maxCarNum;
    //山本加筆　複数のモデルを入れられるように配列化しました
    [SerializeField] private GameObject[] preCar;
    [SerializeField] private int minSpeed;
    [SerializeField] private int maxSpeed;

    private GameObject player;

    private List<GameObject> carList;
    private GameObject[] waypoints;

    private const float instantiateInterval = 0.5f;
    private float carInstantiateTimer;

    private bool firstInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        firstInstantiate = true;

        //子オブジェクト(waypoint)をすべて取得する
        waypoints = GetAllChildObject();

        //endWaypoints = new List<GameObject>();
        carList = new List<GameObject>();

        carInstantiateTimer = instantiateInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstInstantiate)
        {
            firstInstantiate = false;
            for (int i = 0; i < maxCarNum; i++)
            {
                CarInstantiate();
            }
        }

        carInstantiateTimer -= Time.deltaTime;

        //車の数が減ったら補充する
        for (int i = 0; i < carList.Count; i++)
            if (carList[i] == null)
                carList.RemoveAt(i);

        if (carInstantiateTimer <= 0)
        {
            carInstantiateTimer = instantiateInterval;
            // 毎秒2つの車を生成
            if (carList.Count < maxCarNum)
                CarInstantiate();
        }
    }

    //車を生成する
    public void CarInstantiate()
    {
        //山本加筆　モデルを複数の中からランダムに選ぶように変更
        var Car = Instantiate(preCar[Random.Range(0, preCar.Length)]);
        var waypoint = waypoints[Random.Range(0, waypoints.Length)];

        while ((waypoint.transform.position - player.transform.position).magnitude < 100.0f)
        {
            waypoint = waypoints[Random.Range(0, waypoints.Length)];
        }

        Car.GetComponent<Car_R>().Init(waypoint, Random.Range(minSpeed, maxSpeed));

        carList.Add(Car);
    }

    //ウェイポイントを取得する
    private GameObject[] GetAllChildObject()
    {
        GameObject[] objects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            objects[i] = transform.GetChild(i).gameObject;
        }
        return objects;
    }
}
