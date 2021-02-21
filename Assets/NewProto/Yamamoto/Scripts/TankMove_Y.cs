using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove_Y : MonoBehaviour
{
    private EnemyNav_Y navScript;
    public GameObject bulletPrefab;
    private GameObject bullet;
    private GameObject launchPort;
    private GameObject head;
    public float fireFreeze = 5f;
    private float routineTimer = 0f;
    public float desBulletTime = 10f;   //初期値は10秒 Inspector上から変更できます
    public int bulletDamage;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        navScript = GetComponent<EnemyNav_Y>();
        launchPort = transform.Find("Gun").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int HP = GetComponent<ObjectStateManagement_Y>().HP;
        if (HP <= 0) Destroy(this);

        if (navScript.navFlg)
        {
            if (routineTimer <= 0f)
            {
                LaunchBullet();
            }
            else routineTimer -= Time.deltaTime;
        }
    }

    void LaunchBullet()
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
            bullet = Instantiate(bulletPrefab, launchPort.transform.position, transform.rotation);
            bullet.transform.forward = GameObject.Find("Player").transform.position - launchPort.transform.position;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward.normalized * 10f;
            bullet.GetComponent<BulletDamage>().damage = bulletDamage;
            Destroy(bullet, desBulletTime);
        }
    }
}
