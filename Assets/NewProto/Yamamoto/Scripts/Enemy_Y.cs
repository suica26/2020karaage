using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Y : MonoBehaviour
{
    public int HP = 0;
    private bool damage = false;
    private int hitSkilID = 0;
    public int kickDamage = 0;
    public int blastDamage = 0;
    public int cutterDamage = 0;
    private Animator animator;
    private bool live = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0 && live)
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
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
            var chainScript = other.gameObject.GetComponent<ChainBreak_Y>();
            HP -= chainScript.chainDamage;
            hitSkilID = 8;
            damage = true;
        }

        if (damage)
        {
            var dir = other.gameObject.transform.position - transform.position;
            dir.y = 0;
            transform.forward = dir;
            StartCoroutine(DoShake(0.25f, 0.1f));
            damage = false;
        }
    }

    //振動コルーチン
    private IEnumerator DoShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = pos;
    }

    private void Death()
    {
        GetComponent<EnemyNav_Y>().live = false;
        Destroy(GetComponent<EnemyNav_Y>());
        GetComponent<NavMeshAgent>().enabled = false;
        Destroy(this.gameObject, 3f);
        GetComponent<Rigidbody>().useGravity = false;
        if (GetComponent<CapsuleCollider>() != null) Destroy(GetComponent<CapsuleCollider>());
        live = false;
        animator.SetTrigger("Death");
    }
}
