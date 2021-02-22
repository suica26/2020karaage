using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_R : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private int addEP;
    private Parameters_R scrEP;

    void Start()
    {
        scrEP = GameObject.Find("Canvas").GetComponent<Parameters_R>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            scrEP.EPManager(addEP);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
            Destroy(gameObject);
        }
    }
}
