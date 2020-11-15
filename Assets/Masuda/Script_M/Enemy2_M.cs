using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2_M : MonoBehaviour
{
    //enemyTargetにニワトリを入れる
    //障害物（家とか）をよけてニワトリに迫ってくる 
    public GameObject enemyTarget;
    private NavMeshAgent agent;

    void Start()
    {
        //EnemyにNavMeshAgentをくっつけとく
        //家とかをNavigationStaticにする（たぶん）
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = enemyTarget.transform.position;
    }
}
