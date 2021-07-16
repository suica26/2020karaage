using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWaypoint_R : MonoBehaviour
{
    [SerializeField] private GameObject[] nextWaypoint;
    [SerializeField] public bool endWaypoint;
    [SerializeField] public bool uTurn;
    private int num;

    private void Start()
    {
        num = nextWaypoint.Length;
    }

    // 次の交差点を設定する
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

    //次の移動先を設定する
    public Vector3 SetNextTargetPos(Vector3 _nextWaypointPosition, Vector3 _nowWaypointPosition, Vector3 _beforeTargetPos)
    {
        Vector3 vec = transform.position - _nextWaypointPosition;
        Vector3 targetPos = transform.position;
        targetPos.y = _beforeTargetPos.y;           //暫定的処理(高さを固定することで平面的な移動では対応できる。坂道は対応できないので 後で修正)

        // X成分の方が大きい(X方向に移動する)とき
        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.z))
        {
            if (vec.x > 0) // X方向 正
                targetPos.z -= 3;           //Car_Rの数値と同じにすること
            else          // X方向 負
                targetPos.z += 3;

            // 直線移動の時
            if (vec.normalized == (transform.position - _nowWaypointPosition).normalized)
                targetPos.x = _nextWaypointPosition.x;
            else
                targetPos.x = _beforeTargetPos.x;
        }
        else
        {
            if (vec.z > 0) // Z方向 正
                targetPos.x += 3;
            else          // Z方向 負
                targetPos.x -= 3;

            // 直線移動の時
            if (vec.normalized == (transform.position - _nowWaypointPosition).normalized)
                targetPos.z = _nextWaypointPosition.z;
            else
                targetPos.z = _beforeTargetPos.z;
        }
        return targetPos;
    }

    //このWaypointでインスタンス化可能か返す
    public bool Instantiable()
    {
        return endWaypoint && nextWaypoint.Length != 0 ? true : false;
    }
}
