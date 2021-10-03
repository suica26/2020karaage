using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy_Y : MonoBehaviour
{
    public GameObject parachute;
    private bool isFlying;
    private Enemy_Y enemyScr;
    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float rayCycle = 1f;
    private float rayTimer;
    private Collider collider;
    public float fallenSpeed = -1.5f;
    private Stage3BossBuilding_Y st3BossScr;
    private void Start()
    {
        enemyScr = GetComponent<Enemy_Y>();
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();

        enemyScr.enabled = false;
        navMeshAgent.enabled = false;
        animator.enabled = false;
        rb.isKinematic = false;

        Invoke("StartFloating", 2f);
    }

    private void Update()
    {
        rayTimer += Time.deltaTime;

        if (rayTimer >= rayCycle)
        {
            rayTimer = 0f;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 0.3f))
            {
                GroundMode();
                parachute.SetActive(false);
            }
            else FallenMode();
        }

        if (parachute.activeSelf)
            rb.velocity = new Vector3(0, fallenSpeed, 0);
    }

    private void StartFloating()
    {
        collider.isTrigger = false;
        parachute.SetActive(true);
        FallenMode();
    }

    private void FallenMode()
    {
        if (isFlying) return;
        isFlying = true;
        enemyScr.enabled = false;
        animator.enabled = false;
        navMeshAgent.enabled = false;
        rb.isKinematic = false;
    }

    private void GroundMode()
    {
        if (!isFlying) return;
        isFlying = false;
        enemyScr.enabled = true;
        animator.enabled = true;
        navMeshAgent.enabled = true;
        rb.isKinematic = true;
    }
}
