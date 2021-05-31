using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGather : MonoBehaviour
{
    private GameObject player;
    private EvolutionChicken_R scrEvo;
    private Rigidbody rb;

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
        if(DistanceCul() < 5f)
        {
            GoToPlayer();
        }
    }

    private float DistanceCul()
    {
        return(player.transform.position - transform.position).magnitude - 5f * scrEvo.EvolutionNum;
    }

    private void GoToPlayer()
    {
        Vector3 gatherPower = (player.transform.position - transform.position) * 5f;
        rb.AddForce(gatherPower,ForceMode.Acceleration);
    }
}
