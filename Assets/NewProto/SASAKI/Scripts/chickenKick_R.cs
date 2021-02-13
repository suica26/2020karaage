using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenKick_R : MonoBehaviour
{
    [SerializeField] GameObject[] kickCollisions;
    [SerializeField] GameObject kickEffect;
    [SerializeField] AudioClip kickSound;

    AudioSource audioSource;
    EvolutionChicken_R scrEvo;

    public int chargePoint;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scrEvo = GetComponent<EvolutionChicken_R>();

        chargePoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(kickSound);
            var objKick = Instantiate(kickEffect, transform.position, Quaternion.identity);
            Destroy(objKick, 0.5f);
            kickCollisions[scrEvo.EvolutionNum].SetActive(true);
        }
        else
        {
            kickCollisions[scrEvo.EvolutionNum].SetActive(false);
        }
    }
}
