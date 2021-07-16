using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAttack_M : MonoBehaviour
{
    public GameObject atk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject attack = Instantiate(atk, transform.position, Quaternion.identity);
            Destroy(attack, 0.1f);
        }
    }
}
