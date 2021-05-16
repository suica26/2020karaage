using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickID : MonoBehaviour
{

    public int id;
    int time;
    public float cool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && id == 0)
        {

            id = 1;

        }

        if (id == 1)
        {

            time = time + 1;

        }

        if (id == 1 && time >= cool)
        {

            time = 0;
            id = 0;

        }
    }
}
