using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demochicken : MonoBehaviour
{

    private Animator animCon;

    public doorscore door;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        animCon = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (door.nowanim >= 5)
        {

            animCon.SetInteger("a", 0);

            explosion.SetActive(false);

        }
    }
}
