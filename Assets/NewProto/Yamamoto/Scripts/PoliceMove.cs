using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMove : MonoBehaviour
{
    private EnemyNav_Y navScript;
    private float routineTimer = 0f;
    //攻撃をした際の次の行動までの時間
    public float fireFreeze = 3f;    //発砲時
    public float hitFleeze = 1f;    //警棒で殴った時
    public GameObject bulletPrefab = null;
    private string fireStr = "Fire";
    private string hitStr = "Hit";
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        navScript = GetComponent<EnemyNav_Y>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int HP = GetComponent<Enemy_Y>().HP;
        if (HP <= 0) Destroy(this);

        if (navScript.navFlg)
        {
            if (routineTimer <= 0f)
            {
                int value = Random.Range(0, 1 + 1);
                if (value == 0) Fire();
                else if (value == 1) Hit();
            }
            else routineTimer -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        routineTimer = fireFreeze;
        var dir = player.transform.position - transform.position;
        dir.y = 0;  //XZ平面化
        dir = dir.normalized;
        var forwardXZ = transform.forward;
        forwardXZ.y = 0;    //同じく
        forwardXZ = forwardXZ.normalized;

        if (Vector3.Dot(forwardXZ, dir) > 0.5f)   //内積で、オブジェクトがプレイヤーのほうを向いているかを判断
        {
            var genPos = transform.position;
            genPos.y = transform.localScale.y;
            GameObject bullet = Instantiate(bulletPrefab, genPos, transform.rotation);
            bullet.transform.Rotate(90f, 0f, 0f);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * 20f;

            Destroy(bullet, 5f);
        }
    }

    private void Hit()
    {
        routineTimer = hitFleeze;
    }
}
