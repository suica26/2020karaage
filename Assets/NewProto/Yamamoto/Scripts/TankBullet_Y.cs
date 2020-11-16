using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet_Y : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bullet;
    public GameObject player;
    public float desBulletTime = 10f;   //初期値は10秒 Inspector上から変更できます
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaunchBullet(); 
    }

    void LaunchBullet()
    {
        float random = Random.Range(0, 100);
        if (random <= 1)
        {
            bullet = Instantiate(bulletPrefab, this.gameObject.transform.position, this.transform.rotation);
            bullet.transform.forward = player.transform.position - this.transform.position;
            bullet.GetComponent<Rigidbody>().velocity = (bullet.transform.forward.normalized * 10f);
            Invoke("DestroyBullet", desBulletTime);
        }
    }

    void DestroyBullet()
    {
        Destroy(bullet);
    }
}
