using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage0_ButtonFlash_M : MonoBehaviour
{
    public Image kick, cutter;
    public float timer;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "check1")
        {
            timer = Mathf.Sin(Time.time);
            kick.color = new Color(1, 1, 1, timer * 3);
        }
        else if (other.gameObject.tag == "check2")
        {
            timer = Mathf.Sin(Time.time);
            cutter.color = new Color(1, 1, 1, timer * 3);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "check1")
        {
            kick.color = new Color(1, 1, 1, 1);
            timer = 0;
        }
        else if (other.gameObject.tag == "check2")
        {
            cutter.color = new Color(1, 1, 1, 1);
            timer = 0;
        }
    }
}
