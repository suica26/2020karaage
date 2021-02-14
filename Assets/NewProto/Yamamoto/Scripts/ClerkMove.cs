using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClerkMove : MonoBehaviour
{
    private EnemyNav_Y navScript;
    [SerializeField]private float routineTimer;
    //攻撃をした際の次の行動までの時間
    public float hitFleeze = 5f;    //殴り時
    private Animator animator;
    private string waitStr = "isWait";
    private string runStr = "isRun";
    private string hitStr = "isHit";
    private GameObject player;
    public GameObject hitBoxPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        navScript = GetComponent<EnemyNav_Y>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        routineTimer = 0f;
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
                if (Vector3.Distance(player.transform.position, transform.position) <= 5f)
                {
                    Hit();
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
        animator.SetBool(hitStr, false);
    }

    private void Run()
    {
        animator.SetBool(runStr, true);
        animator.SetBool(waitStr, false);
        animator.SetBool(hitStr, false);
        transform.forward = navScript.nav.velocity;
    }

    private void Hit()
    {
        routineTimer = hitFleeze;
        animator.SetBool(hitStr, true);
        Debug.Log("hit");
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
