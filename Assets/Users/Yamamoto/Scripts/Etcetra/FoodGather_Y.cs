using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGather_Y : MonoBehaviour
{
    private static GameObject player;
    private static EvolutionChicken_R scrEvo;
    private Rigidbody rb;
    public float gatherDistance;
    public float gatherSpeed;
    public float centripetalRatio;  //向心力の比率

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
            player = GameObject.Find("Player");
        if(scrEvo == null)
            scrEvo = player.GetComponent<EvolutionChicken_R>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position - transform.position).magnitude - 3f * scrEvo.EvolutionNum < gatherDistance)
        {
            GoToPlayer();
        }
    }

    private void GoToPlayer()
    {
        Vector3 fallenSpeed = new Vector3(0, rb.velocity.y, 0);
        Vector3 toPlayer = (player.transform.position - transform.position).normalized;
        Vector3 gatherDir = (Quaternion.Euler(0, 145, 0) * toPlayer + toPlayer * centripetalRatio).normalized;
        rb.velocity = gatherDir * gatherSpeed * (scrEvo.EvolutionNum + 1) + fallenSpeed;
    }
}
