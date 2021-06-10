using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGather_Y : MonoBehaviour
{
    private GameObject player;
    private EvolutionChicken_R scrEvo;
    private Rigidbody rb;
    public float gatherDistance;
    public float gatherSpeed;
    public float centripetalRatio;  //向心力の比率

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scrEvo = player.GetComponent<EvolutionChicken_R>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DistanceCul() < gatherDistance)
        {
            GoToPlayer();
        }
    }

    private float DistanceCul()
    {
        return (player.transform.position - transform.position).magnitude - 5f * scrEvo.EvolutionNum;
    }

    private void GoToPlayer()
    {

        Vector3 toPlayer = (player.transform.position - transform.position).normalized;
        Vector3 gatherDir = (Quaternion.Euler(0, 145, 0) * toPlayer + toPlayer * centripetalRatio).normalized;
        rb.velocity = gatherDir * gatherSpeed;
    }
}
