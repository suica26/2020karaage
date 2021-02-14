using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Y : MonoBehaviour
{
    public int HP = 0;
    private bool damage = false;
    private int hitSkilID = 0;
    public int kickDamage = 0;
    public int blastDamage = 0;
    public int cutterDamage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            Destroy(this.gameObject, 3f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //キックダメージ
        if (other.gameObject.name == "KickCollision")
        {
            HP -= kickDamage;
            hitSkilID = 1;
            damage = true;
        }
        //ブラストダメージ
        if (other.gameObject.name == "MorningBlastSphere_Y(Clone)")
        {
            HP -= blastDamage;
            hitSkilID = 2;
            damage = true;
        }
        //カッターダメージ
        if (other.gameObject.name == "Cutter(Clone)")
        {
            HP -= cutterDamage;
            hitSkilID = 3;
            damage = true;
        }
        if (other.gameObject.tag == "Chain")
        {
        }

        if (damage)
        {
        }
    }

    private void Death()
    {

    }
}
