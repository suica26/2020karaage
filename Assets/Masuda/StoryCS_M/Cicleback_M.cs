using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cicleback_M : MonoBehaviour
{
    [SerializeField] GameObject objParam;
    [SerializeField] private Image image1, image2, image3, image4, gauge;
    private int esa, line1, line2, line3;
    private Parameters_R scr;
    void Start()
    {
        scr = objParam.gameObject.GetComponent<Parameters_R>();
        gauge.enabled = true;
        image1.enabled = true;
        image2.enabled = false;
        image3.enabled = false;
        image4.enabled = false;
        line1 = scr.evo1;
        line2 = scr.evo2;
        line3 = scr.evo3;
    }

    void Update()
    {
        esa = scr.ep;
        if (esa == line1)
        {
            image1.enabled = false;
            image2.enabled = true;
        }
        else if(esa == line2)
        {
            image2.enabled = false;
            image3.enabled = true;
        }
        else if (esa == line3)
        {
            image3.enabled = false;
            image4.enabled = true;
        }
    }
}
