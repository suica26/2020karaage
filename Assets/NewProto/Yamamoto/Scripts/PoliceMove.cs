using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMove : MonoBehaviour
{
    private EnemyNav_Y navScript;
    [SerializeField]private float routineTimer = 0f;
    //攻撃をした際の次の行動までの時間
    public float fireFreeze = 3f;    //発砲時
    public float hitFleeze = 1f;    //警棒で殴った時
    public GameObject bulletPrefab = null;
    private Animator animator;
    private string waitStr = "isWait";
    private string runStr = "isRun";
    private string fireStr = "Fire";
    private string hitStr = "Hit";
    private GameObject player;
    public GameObject hitBoxPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        navScript = GetComponent<EnemyNav_Y>();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int HP = GetComponent<Enemy_Y>().HP;
        if (HP <= 0) Destroy(this);

        if (navScript.navFlg)
        {
            Run();

            if (routineTimer <= 0f)
            {
                float dist = Vector3.Distance(player.transform.position, transform.position);
                if (dist <= 20f)
                {
                    Hit();
                }
                else if (dist <= 30f && dist > 20f)
                {
                    FireSet();
                }
                else routineTimer = 3f;
            }
            else
            {
                routineTimer -= Time.deltaTime;
            }
        }
        else
        {
            Wait();
        }
    }
    private void Wait()
    {
        animator.SetBool(waitStr, true);
        animator.SetBool(runStr, false);
    }

    private void Run()
    {
        animator.SetBool(runStr, true);
        animator.SetBool(waitStr, false);
    }

    private void FireSet()
    {
        routineTimer = fireFreeze;
        var dir = player.transform.position - transform.position;
        dir.y = 0;  //XZ平面化
        dir = dir.normalized;
        var forwardXZ = transform.forward;
        forwardXZ.y = 0;    //同じく
        forwardXZ = forwardXZ.normalized;

        if (Vector3.Dot(forwardXZ, dir) > 0.5f)   //内積で、オブジェクトがプレイヤーのほうを向いているかを判断
        {
            animator.SetTrigger(fireStr);
        }
    }

    private void Fire()
    {
        var genPos = transform.position;
        genPos.y = transform.localScale.y;
        GameObject bullet = Instantiate(bulletPrefab, genPos, transform.rotation);
        bullet.transform.Rotate(90f, 0f, 0f);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
        Destroy(bullet, 5f);
    }

    private void Hit()
    {
        routineTimer = hitFleeze;
        animator.SetTrigger(hitStr);
    }

    private void CreateDamageBox()
    {
        var genPos = transform.position + transform.forward * 2f;
        genPos.y = 2f;
        var hitBox = Instantiate(hitBoxPrefab, genPos, Quaternion.identity, transform.Find("Body"));
        hitBox.GetComponent<BoxCollider>().isTrigger = true;
        Destroy(hitBox, Time.deltaTime * 10f);
    }
}
