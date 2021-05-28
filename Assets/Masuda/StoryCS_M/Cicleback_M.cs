using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cicleback_M : MonoBehaviour
{
    [SerializeField] GameObject objParam;
    [SerializeField] private Image image1, image2, image3, image4, gauge;
    private int esa;
    private Parameters_R scr;
    void Start()
    {
        scr = objParam.gameObject.GetComponent<Parameters_R>();
        gauge.enabled = true;
        image1.enabled = true;
        image2.enabled = false;
        image3.enabled = false;
        image4.enabled = false;
    }

    void Update()
    {
        esa = scr.ep;
        if (esa == 30)
        {
            image1.enabled = false;
            image2.enabled = true;
        }
        else if(esa == 100)
        {
            image2.enabled = false;
            image3.enabled = true;
        }
        else if (esa == 300)
        {
            image3.enabled = false;
            image4.enabled = true;
        }
    }
}
