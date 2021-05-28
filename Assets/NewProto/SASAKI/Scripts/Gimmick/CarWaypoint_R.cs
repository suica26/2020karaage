using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWaypoint_R : MonoBehaviour
{
    [SerializeField] private GameObject[] nextWaypoint;
    [SerializeField] public bool endWaypoint;
    [SerializeField] private bool uTurn;
    private int num;

    private void Start()
    {
        num = nextWaypoint.Length;
    }

    //次の交差点を設定する
    public GameObject SetNextWaypoint(GameObject _nowWaypoint)
    {
        GameObject waypoint = _nowWaypoint;

        if (!uTurn)
        {
            while (waypoint == _nowWaypoint)
            {
                waypoint = nextWaypoint[Random.Range(0, num)];
            }
        }
        else
        {
            waypoint = nextWaypoint[Random.Range(0, num)];
        }

        return waypoint;
    }

    //車の移動先を決定する
    public Vector3 SetDistination()
    {
        return transform.position;
    }
}
