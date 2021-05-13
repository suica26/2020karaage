using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroytest_M : MonoBehaviour
{
    [SerializeField] private GameObject Text,Bill,Objct;
    //[SerializeField] private int score;

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("jama"))
        {
            Bill.SetActive(false);
            Text.SetActive(true);
            //Destroy(Objct, 1.0f);
        }
    }
}
