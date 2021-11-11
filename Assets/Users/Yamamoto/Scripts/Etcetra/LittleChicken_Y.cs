using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleChicken_Y : MonoBehaviour
{
    private static List<GameObject> trackList;
    private static List<Rigidbody> trackRigidList;
    private static GameObject player;
    private static EvolutionChicken_R evoScr;
    [SerializeField] private int myTrackNumber;
    private float accelT;
    private float[] checkDist = new float[4] { 1f, 4f, 13f, 15f };
    private float trackSpeed;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        if (player == null) player = GameObject.Find("Player");
        //リストの初期化
        //完全に初めての場合
        if (trackList == null)
        {
            InitializeTracker();
        }
        else    //ステージのやり直しなど、初めてではない場合
        {
            foreach (var t in trackList)
            {
                if (t == null)
                    InitializeTracker();
                break;
            }
        }
        trackList.Add(gameObject);
        trackRigidList.Add(GetComponent<Rigidbody>());
        myTrackNumber = trackList.Count - 1;
    }

    // Update is called once per frame
    private void Update()
    {
        float dist = (trackList[myTrackNumber - 1].transform.position - transform.position).magnitude;
        float checker;
        if (myTrackNumber == 1) checker = checkDist[evoScr.EvolutionNum] * 1.1f;
        else checker = 5f;
        if (dist >= checker) Accelerate();
        else Decelerate();
    }
    private void InitializeTracker()
    {
        Debug.Log("Initialize tracker");
        evoScr = player.GetComponent<EvolutionChicken_R>();
        trackList = new List<GameObject>();
        trackRigidList = new List<Rigidbody>();
        trackList.Add(player);
        trackRigidList.Add(player.GetComponent<Rigidbody>());
    }

    private void Accelerate()
    {
        var dir = (trackList[myTrackNumber - 1].transform.position - transform.position).normalized;
        transform.forward = new Vector3(dir.x, 0f, dir.z);
        animator.SetBool("IsWalk", true);

        accelT += Time.deltaTime;
        if (accelT >= 1f) accelT = 1f;
        trackRigidList[myTrackNumber].velocity = dir * evoScr.Status_SPD * accelT;
    }

    private void Decelerate()
    {
        var dir = (trackList[myTrackNumber - 1].transform.position - transform.position).normalized;
        transform.forward = new Vector3(dir.x, 0f, dir.z);

        accelT -= Time.deltaTime * 5;
        if (accelT <= 0f)
        {
            accelT = 0f;
            animator.SetBool("IsWalk", false);
        }
        else
        {
            trackRigidList[myTrackNumber].velocity = dir * evoScr.Status_SPD * accelT;
            animator.SetBool("IsWalk", true);
        }
    }
}
