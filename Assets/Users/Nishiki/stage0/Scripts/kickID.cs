using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickID : MonoBehaviour
{
    public int id;
    int time;
    public float cool;
    public GameObject colider;
    public doorscore door;

    public int boxscore;

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
            colider.SetActive(true);
        }

        if (id == 1 && time >= cool)
        {
            time = 0;
            id = 0;
        }

        if (id == 0)
        {
            colider.SetActive(false);
        }

        if (door.nowanim >= 5)
        {
            id = 1;
        }
    }
}
