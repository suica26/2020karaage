using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    //弾丸に必ずつけてください

    public int damage;
    private Parameters_R param;
    private bool isHit = false;

    void Start()
    {
        param = GameObject.Find("Canvas").GetComponent<Parameters_R>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isHit)
        {
            isHit = true;
            param.HPManager(damage);
        }
        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "WayPoint")
            Destroy(gameObject);
    }
}
