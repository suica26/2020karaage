using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_R : MonoBehaviour
{
    [SerializeField] private bool carMoving;    //車が走行するか否か
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;

    private GameObject nowWaypoint;
    private GameObject nextWaypoint;
    private Vector3 targetPos;

    public void Init(GameObject obj, int _speed)
    {
        speed = _speed;
        nextWaypoint = obj;
        transform.position = nextWaypoint.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(carMoving)
        {
            //交差点情報更新
            nowWaypoint = nextWaypoint;

            nextWaypoint = nextWaypoint.GetComponent<CarWaypoint_R>().SetNextWaypoint(nowWaypoint);
            targetPos = nextWaypoint.GetComponent<CarWaypoint_R>().SetDistination();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(carMoving)
        {
            CarMove();
        }
    }

    //車の動き
    private void CarMove()
    {
        if ((transform.position - targetPos).magnitude > 0.5f)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(newPos - transform.position), rotSpeed * Time.deltaTime);
            transform.position = newPos;  
        }    
        else
        {
            if(nextWaypoint.GetComponent<CarWaypoint_R>().endWaypoint)
            {
                Destroy(this.gameObject);
            }
            else
            {
                //交差点情報更新
                GameObject before = nowWaypoint;
                nowWaypoint = nextWaypoint;

                nextWaypoint = nextWaypoint.GetComponent<CarWaypoint_R>().SetNextWaypoint(before);
                targetPos = nextWaypoint.GetComponent<CarWaypoint_R>().SetDistination();
            }
        }
    }

    private void Brakes()
    {

    }
}
