using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_R : MonoBehaviour
{
    [SerializeField] private bool carMoving;    //車が走行するか否か
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float initHeight;  //車種ごとに高さを設定して浮かないようにする(暫定的処理)

    private GameObject nowWaypoint;         //現在いる交差点
    private GameObject nextWaypoint;        //次の交差点
    private GameObject afterNextWaypoint;   //その次の交差点
    private Vector3 targetPos;
    private bool isUturn;                   // Uターンに入ったか否か
    private bool isFirstMeetEnd;            // 生成時にすぐに消えてしまうことを防ぐ

    public void Init(GameObject obj, int _speed)
    {
        isFirstMeetEnd = true;
        isUturn = false;
        speed = _speed;
        nextWaypoint = obj;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (carMoving)
        {
            // 交差点情報更新
            nowWaypoint = nextWaypoint;

            // 次の交差点と　その次の交差点を取得
            nextWaypoint = nowWaypoint.GetComponent<CarWaypoint_R>().SetNextWaypoint(nowWaypoint);
            afterNextWaypoint = nextWaypoint.GetComponent<CarWaypoint_R>().SetNextWaypoint(nowWaypoint);

            //初期移動先(インスタンス先)を設定
            SetPosInit(nowWaypoint.transform.position);
            targetPos.y += initHeight;
            transform.position = targetPos;

            if (!nextWaypoint.GetComponent<CarWaypoint_R>().uTurn)
            {
                targetPos = afterNextWaypoint.GetComponent<CarWaypoint_R>().SetNextTargetPos(nextWaypoint.transform.position, nowWaypoint.transform.position, targetPos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (carMoving)
        {
            //前に車がいたら停止
            RaycastHit[] hits = Physics.RaycastAll(transform.position + transform.forward * 0.5f, transform.forward, 5.0f);
            foreach (var obj in hits)
            {
                if (obj.transform.tag == "Car")
                    return;
            }
            CarMove();
        }
    }

    //車の動き
    private void CarMove()
    {
        if ((transform.position - targetPos).magnitude > 0.5f)   // 移動先との距離が規定値より遠いとき
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (newPos - transform.position != Vector3.zero)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((newPos - transform.position) * 10), rotSpeed * Time.deltaTime);
            }
            transform.position = newPos;
        }
        else                                                     // 移動先との距離が規定値より近いとき
        {
            // 次の交差点が終端の場合
            if (nowWaypoint.GetComponent<CarWaypoint_R>().endWaypoint)
            {
                if (isFirstMeetEnd)
                    isFirstMeetEnd = false;
                else
                    Destroy(this.gameObject);
            }

            // Uターンの際に情報の更新を一回の移動分止める(これをしないと経路情報がバグる)
            if ((afterNextWaypoint.GetComponent<CarWaypoint_R>().uTurn || nextWaypoint.GetComponent<CarWaypoint_R>().uTurn) && !isUturn)
                isUturn = true;
            else
            {
                isUturn = false;

                //交差点情報更新
                nowWaypoint = nextWaypoint;
                nextWaypoint = afterNextWaypoint;
            }

            //次の交差点がUターンの時
            if (nextWaypoint.GetComponent<CarWaypoint_R>().uTurn)
            {
                SetPosInit(nextWaypoint.transform.position);
                if (isUturn)
                {
                    afterNextWaypoint = nextWaypoint.GetComponent<CarWaypoint_R>().SetNextWaypoint(nowWaypoint);
                    targetPos = afterNextWaypoint.GetComponent<CarWaypoint_R>().SetNextTargetPos(nextWaypoint.transform.position, nowWaypoint.transform.position, targetPos);
                }
            }
            else
            {
                //次の次の交差点が終端である場合
                if (afterNextWaypoint.GetComponent<CarWaypoint_R>().endWaypoint)
                {
                    targetPos = afterNextWaypoint.GetComponent<CarWaypoint_R>().SetNextTargetPos(nextWaypoint.transform.position, nowWaypoint.transform.position, targetPos);
                }
                else
                {
                    afterNextWaypoint = nextWaypoint.GetComponent<CarWaypoint_R>().SetNextWaypoint(nowWaypoint);
                    targetPos = afterNextWaypoint.GetComponent<CarWaypoint_R>().SetNextTargetPos(nextWaypoint.transform.position, nowWaypoint.transform.position, targetPos);
                }
            }

            //山本加筆　ちょっとすごい頻度でDebug.Logに出てきちゃうので、コメントアウトしときます
            //Debug.Log("CAR WAYPOINT LOG: " + nowWaypoint + " -> " + nextWaypoint + " -> " + afterNextWaypoint);
        }
    }

    private void SetPosInit(Vector3 _origin)
    {
        Vector3 vec = nextWaypoint.transform.position - nowWaypoint.transform.position;
        targetPos = _origin;

        // X成分の方が大きい(X方向に移動する)とき
        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.z))
        {
            if (vec.x > 0) // X方向 正
            {
                targetPos.z -= 3;       //CarWaypoint_Rと同じ数値にすること
            }
            else          // X方向 負
            {
                targetPos.z += 3;
            }
        }
        else
        {
            if (vec.z > 0) // Z方向 正
            {
                targetPos.x += 3;
            }
            else          // Z方向 負
            {
                targetPos.x -= 3;
            }
        }
    }

    private void Brakes()
    {
        //どっかで実装したい
    }
}
