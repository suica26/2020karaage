﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civil_Y : MonoBehaviour
{
    //市民を実装する際には、必ずTagでCivilを設定すること
    public bool escapeFlg = false;
    public float escapeSpeed;
    public float walkSpeed;
    public float contagionRange;
    private Vector3 fallenSpeed;
    private Rigidbody rb;
    private GameObject player;
    public GameObject[] route;
    private int routeNum = 0;
    private Vector3 preForward;
    private Vector3 nextForward;
    private float rotSpeed;
    private float rotTime = 0f;
    //市民の内部時間
    private float deleteTimer;
    private float timer;
    [SerializeField] private float deleteTiming;
    [SerializeField] private float resetForwardTiming;
    public WayPointGraph_Y wayPointGraph;
    public bool avoidFlg = false;
    [SerializeField] private Animator animator;
    private string escapeStr = "isEscape";

    private bool surprised;
    private bool death;

    //ADX
    private CriAtomSource criAtomSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        rotSpeed = Random.Range(0.7f, 1.5f);
        criAtomSource = GetComponent<CriAtomSource>();
        wayPointGraph = GameObject.Find("GameAI_Y").GetComponent<WayPointGraph_Y>();
        surprised = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!death)
        {
            deleteTimer += Time.deltaTime;
            timer += Time.deltaTime;

            if (!surprised)
            {
                //迷子(次のWayPointに到着できなかった)になった時に自分を消去する処理
                if (deleteTimer > deleteTiming) Death();
                //進行方向を一定タイミングで再計算する
                if (timer > resetForwardTiming) ResetNextForward();

                if (escapeFlg) Escape();
                else Walk();
            }
            else
            {
                if (timer > 1f)
                {
                    surprised = false;
                    rb.isKinematic = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!escapeFlg)
        {
            //ダメージを受けると逃げるフラグがたつ
            if (other.gameObject.name == "KickCollision" ||
                other.gameObject.name == "MorningBlastSphere_Y(Clone)" ||
                other.gameObject.name == "Cutter(Clone)" ||
                other.gameObject.tag == "Chain" ||
                other.gameObject.name == "fallAttackCircle(Clone)")
            {
                EscapeContagion();
                criAtomSource.Play("Citizen00");
            }
        }

        if (other.gameObject == route[routeNum] && !escapeFlg)
        {
            //ルート進行
            routeNum++;//最終地点に到達したら、オブジェクト消去
            if (routeNum == route.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Arrive");
                rotTime = 0f;
                deleteTimer = 0f;
                preForward = nextForward;
                nextForward = GetVectorXZNormalized(route[routeNum].transform.position, route[routeNum - 1].transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<EvolutionChicken_R>().EvolutionNum > 1)
            {
                //キックとほぼ同じ
                GetComponentInChildren<Collider>().isTrigger = true;
                var D = (transform.position - other.gameObject.transform.position).normalized;   //力の方向
                D = new Vector3(D.x, 0.2f, D.z);
                var F = D * 500f;
                Vector3 TorquePower = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f));
                rb.AddForce(F, ForceMode.Impulse);
                rb.AddTorque(TorquePower, ForceMode.Impulse);
                Destroy(gameObject, 2f);
                Escape();
                death = true;
            }
            else
            {
                //その場で停止する
                transform.forward = GetVectorXZNormalized(other.transform.position, transform.position);
                surprised = true;
                timer = 0f;
                rb.isKinematic = true;
                rotTime = 0f;
            }
        }
    }

    private void EscapeContagion()
    {
        Escape();
        var civilObjects = GameObject.FindGameObjectsWithTag("Civil");
        foreach (var civils in civilObjects)
        {
            if (GetVectorXZ(transform.position, civils.transform.position).magnitude < contagionRange)
            {
                civils.GetComponent<Civil_Y>().Escape();
            }
        }
    }

    //XZ平面における点Bから点Aへの方向ベクトルを返す。(yは0)
    private Vector3 GetVectorXZ(Vector3 A, Vector3 B)
    {
        var AB = A - B;
        AB.y = 0;
        return AB;
    }

    //GetVectorXZの単位ベクトル化まで
    private Vector3 GetVectorXZNormalized(Vector3 A, Vector3 B)
    {
        var AB = A - B;
        AB.y = 0;
        AB = AB.normalized;
        return AB;
    }

    public void Escape()
    {
        Debug.Log("Escape!");
        animator.SetBool(escapeStr, true);
        if (!escapeFlg) escapeFlg = true;
        if (!avoidFlg)
        {
            transform.forward = GetVectorXZNormalized(transform.position, player.transform.position);
        }

        //y方向の速度を保存
        fallenSpeed = new Vector3(0, rb.velocity.y, 0);
        //自分の正面方向に進むように設定
        rb.velocity = transform.forward * escapeSpeed + fallenSpeed;
    }

    private void Walk()
    {
        //y方向の速度を保存
        fallenSpeed = new Vector3(0, rb.velocity.y, 0);
        //自分の正面方向に進むように設定
        rb.velocity = transform.forward * walkSpeed + fallenSpeed;

        if (Vector3.Dot(transform.forward, nextForward) < 1f && !avoidFlg)
        {
            RotTimePlus();
            transform.forward = Vector3.Lerp(preForward, nextForward, rotTime);
        }
    }

    public void RouteSetting(GameObject[] setRoute)
    {
        route = setRoute;
        transform.forward = GetVectorXZNormalized(route[routeNum].transform.position, transform.position);
        nextForward = transform.forward;
    }

    private void RotTimePlus()
    {
        if (rotTime < 1.0f)
        {
            rotTime += rotSpeed * Time.deltaTime;
        }
        else if (rotTime > 1.0f)
        {
            rotTime = 1.0f;
        }
    }

    public void Avoid(GameObject other)
    {
        rotTime = 0f;
        float cross_y = Vector3.Cross(transform.forward, GetVectorXZNormalized(other.transform.position, transform.position)).y;
        if (cross_y > 0f)
        {
            transform.Rotate(0, -5, 0);
        }
        else if (cross_y < 0f)
        {
            transform.Rotate(0, -5, 0);
        }
    }

    public void ResetForward()
    {
        preForward = transform.forward;
        rotTime = 0f;
    }

    private void Death()
    {
        wayPointGraph.CivilNumDecrease();
        Destroy(gameObject);
    }

    private void ResetNextForward()
    {
        timer = 0f;
        nextForward = GetVectorXZNormalized(route[routeNum].transform.position, transform.position);
    }
}
