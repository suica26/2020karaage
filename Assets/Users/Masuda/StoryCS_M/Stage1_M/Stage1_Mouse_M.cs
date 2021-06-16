using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1_Mouse_M : MonoBehaviour
{
    [SerializeField] Renderer shop;
    [SerializeField] GameObject mouse;
    //[SerializeField] Animation mouseChange;
    void Start()
    {
        shop = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shop.isVisible)
        {
            mouse.SetActive(true);
            //mouseChange.Play();
            Debug.Log("見えてる");
        }
        else
        {
            mouse.SetActive(false);
            //mouseChange.Stop();
        }
    }
}
