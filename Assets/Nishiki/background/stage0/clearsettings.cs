using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearsettings : MonoBehaviour
{

    public GameObject whiteness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            whiteness.SetActive(true);

        }
    }
}
