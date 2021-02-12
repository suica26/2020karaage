using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorBlastAffection_R : MonoBehaviour
{
    MorBlast_R morBlaScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject chiken = GameObject.FindGameObjectWithTag("Player");
        morBlaScript = chiken.GetComponent<MorBlast_R>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet_Y(Clone)")
        {
            if (morBlaScript.Number == 0)
            {
                ReflectBullet(other.gameObject);
            }
            else if (morBlaScript.Number == 1)
            {
                ExplodeBullet(other.gameObject);
            }
            else if (morBlaScript.Number == 2)
            {
                ShootDownBullet(other.gameObject);
            }
        }
    }

    public void ReflectBullet(GameObject bullet)
    {
        bullet.gameObject.GetComponent<Rigidbody>().velocity = (bullet.transform.position - this.transform.position).normalized * 15f;
    }

    public void ExplodeBullet(GameObject bullet)
    {
        Destroy(bullet);
    }

    public void ShootDownBullet(GameObject bullet)
    {
        bullet.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
