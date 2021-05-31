using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_R : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private int addEP;
    private Parameters_R scrEP;
    //ADX
    private new CriAtomSource audio;

    void Start()
    {
        scrEP = GameObject.Find("Canvas").GetComponent<Parameters_R>();
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            audio.Play("FeedGet00");
            scrEP.EPManager(addEP);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
            Destroy(gameObject);
        }
    }
}
