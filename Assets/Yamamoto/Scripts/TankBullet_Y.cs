using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet_Y : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaunchBullet(); 
        this.transform.position += transform.forward;
    }

    void LaunchBullet()
    {
        float random = Random.Range(0, 100);
        if (random <= 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, this.gameObject.transform.position, this.transform.rotation);

            bullet.GetComponent<Rigidbody>().velocity = (bullet.transform.forward.normalized);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this);
        }
    }
}
