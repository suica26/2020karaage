using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode_R : MonoBehaviour
{
    public float force;
    public int usageEvo;
    Rigidbody rigid;
    EvolutionChicken_R scrEvo;
    void Start()
    {
        rigid = GameObject.Find("Player").GetComponent<Rigidbody>();
        scrEvo = GameObject.Find("Player").GetComponent<EvolutionChicken_R>();
    }

    //周辺のオブジェクトに影響を与える
    private void OnTriggerStay(Collider other)
    {
        Vector3 pos = transform.position - new Vector3(0, 3, 0);
        if(other.gameObject.tag == "Player" && usageEvo >= scrEvo.EvolutionNum)
        {
            rigid.velocity = Vector3.zero;
            Vector3 XZmag = new Vector3(other.transform.position.x - pos.x, 0, other.transform.position.z - pos.z);
            rigid.AddForce((XZmag + Vector3.up * 0.5f) * force, ForceMode.Impulse);
            Destroy(this);
        }
    }
}
