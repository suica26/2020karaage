using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    //弾丸に必ずつけてください

    public int damage;
    private Parameters_R param;

    void Start()
    {
        param = GameObject.Find("Canvas").GetComponent<Parameters_R>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            param.HPManager(damage);
        }

        if(other.gameObject.tag != "Enemy" && other.gameObject.tag != "EnemyItem")
        {
            Destroy(this.gameObject);
        }
    }
}
