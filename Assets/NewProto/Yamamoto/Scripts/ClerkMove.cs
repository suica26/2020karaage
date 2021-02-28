using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClerkMove : MonoBehaviour
{
    private EnemyNav_Y navScript;
    [SerializeField]private float routineTimer = 0f;
    //攻撃をした際の次の行動までの時間
    public float hitFleeze = 5f;    //殴り時
    private Animator animator;
    private string hitStr = "Hit";
    private GameObject player;
    private NavMeshAgent agent;
    private GameObject hitBox;
    public GameObject hitBoxPrefab = null;
    public int hitDamage;

    // Start is called before the first frame update
    void Start()
    {
        navScript = GetComponent<EnemyNav_Y>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int HP = GetComponent<Enemy_Y>().HP;
        if (HP <= 0) Destroy(this);
        animator.SetFloat("Speed", agent.speed);

        if (navScript.navFlg)
        {
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
    }

    private void Hit()
    {
        routineTimer = hitFleeze;
        animator.SetTrigger(hitStr);
    }

    private void CreateDamageBox()
    {
        var genPos = transform.position + transform.forward * 2f;
        genPos.y = 0.5f;
        hitBox = Instantiate(hitBoxPrefab, genPos, Quaternion.identity, transform.Find("Body"));
        hitBox.GetComponent<BoxCollider>().isTrigger = true;
        hitBox.GetComponent<HitBoxDamage>().damage = hitDamage;
    }

    private void DeleteHitBox()
    {
        Destroy(hitBox);
    }
}
