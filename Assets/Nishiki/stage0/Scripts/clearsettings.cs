using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearsettings : MonoBehaviour
{

    public GameObject whiteness;
    public GameObject Schanger;
    public int mode = 0;
    int time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 1)
        {

            time = time + 1;

        }

        if (time >= 80)
        {

            whiteness.SetActive(true);

        }

        if (time >= 300)
        {

            Schanger.SetActive(true);

        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            mode = 1;

        }
    }
}
