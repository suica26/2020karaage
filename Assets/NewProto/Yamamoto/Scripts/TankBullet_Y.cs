using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet_Y : MonoBehaviour
{
    private EnemyNav_Y navScript;
    public GameObject bulletPrefab;
    private GameObject bullet;
    private GameObject launchPort;
    private GameObject head;
    public float fireFreeze = 5f;
    private float routineTimer = 0f;
    public float desBulletTime = 10f;   //初期値は10秒 Inspector上から変更できます
    // Start is called before the first frame update
    void Start()
    {
        navScript = this.gameObject.GetComponent<EnemyNav_Y>();
        launchPort = transform.Find("Gun/launchport").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (navScript.navFlg)
        {
            if (routineTimer <= 0f)
            {
                Debug.Log(routineTimer);
                LaunchBullet();
            }
            else routineTimer -= Time.deltaTime;
        }
    }

    void LaunchBullet()
    {
        routineTimer = fireFreeze;
        bullet = Instantiate(bulletPrefab, launchPort.transform.position, this.transform.rotation);
        bullet.transform.forward = GameObject.Find("Player").transform.position - launchPort.transform.position;
        bullet.GetComponent<Rigidbody>().velocity = (bullet.transform.forward.normalized * 10f);
        Destroy(bullet, desBulletTime);
    }
}
