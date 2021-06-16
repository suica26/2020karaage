using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemer : MonoBehaviour
{
    public GameObject coin;
    public GameObject smoke;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(coin, transform.position, coin.transform.rotation);
        Instantiate(smoke, transform.position, smoke.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
