using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode_R : MonoBehaviour
{
    public float force;
    public int usageEvo;
    private float timer;
    Rigidbody rigid;
    EvolutionChicken_R scrEvo;
    void Start()
    {
        timer = 0.0f;
        rigid = GameObject.Find("Player").GetComponent<Rigidbody>();
        scrEvo = GameObject.Find("Player").GetComponent<EvolutionChicken_R>();
    }

    private void Update()
    {
        if (timer <= 0.5f)
            timer += Time.deltaTime;
        else
            Destroy(this);
    }

    //周辺のオブジェクトに影響を与える
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && usageEvo >= scrEvo.EvolutionNum)
        {
            rigid.AddExplosionForce(force, transform.position - new Vector3(0.0f, 3.0f, 0.0f), 50.0f, 1.0f, ForceMode.Impulse);
            //Destroy(this);
        }
    }
}
