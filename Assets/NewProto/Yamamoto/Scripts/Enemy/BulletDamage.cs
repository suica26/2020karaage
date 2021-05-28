using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    //弾丸に必ずつけてください
    //コミット打ち消し用の文なので、後で消します

    public int damage;
    private Parameters_R param;

    void Start()
    {
        param = GameObject.Find("Canvas").GetComponent<Parameters_R>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            param.HPManager(damage);
            Destroy(this.gameObject);
        }
    }
}
