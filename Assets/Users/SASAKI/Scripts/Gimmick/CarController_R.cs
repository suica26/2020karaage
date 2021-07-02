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

    private List<GameObject> carList;
    private List<GameObject> endWaypoints;

    private const float instantiateInterval = 0.5f;
    private float carInstantiateTimer;

    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクト(waypoint)をすべて取得する
        GameObject[] waypoints = GetAllChildObject();

        endWaypoints = new List<GameObject>();
        carList = new List<GameObject>();

        //車を生成可能なウェイポイントのみを選別してリスト化
        foreach (GameObject waypoint in waypoints)
        {
            if (waypoint.GetComponent<CarWaypoint_R>().Instantiable())
            {
                endWaypoints.Add(waypoint);
            }
        }

        carInstantiateTimer = instantiateInterval;
    }

    // Update is called once per frame
    void Update()
    {
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
        var waypoint = endWaypoints[Random.Range(0, endWaypoints.Count)];
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
