using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGather_Y : MonoBehaviour
{
    private GameObject player;
    private EvolutionChicken_R scrEvo;
    private Rigidbody rb;
    public float gatherSpeed;

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
        if (DistanceCul() < 20f)
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
        transform.forward = toPlayer;
        //速度のY成分を保持(一応)
        var currentSpeed_Y = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = toPlayer * gatherSpeed * Time.deltaTime + currentSpeed_Y;
    }
}
