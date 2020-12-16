using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorBlaAffection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet_Y(Clone)")
        {
            other.gameObject.GetComponent<Bullet_Y>().ReflectBullet(this.gameObject);
        }
    }
}
