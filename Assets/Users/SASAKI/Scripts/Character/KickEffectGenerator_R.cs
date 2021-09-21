using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickEffectGenerator_R : MonoBehaviour
{
    [SerializeField] private GameObject kickEffect;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Big")
        {
            GameObject obj = Instantiate(kickEffect, transform.position, Camera.main.transform.rotation);
            Destroy(obj, 1.0f);
        }
    }
}
