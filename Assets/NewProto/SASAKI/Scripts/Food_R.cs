using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_R : MonoBehaviour
{
    [SerializeField] private AudioClip sound = null;
    public GameObject Player = null;
    public Parameters_R scrEP = null;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            scrEP.EPManager(10);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
            Destroy(gameObject);
        }
    }
}
