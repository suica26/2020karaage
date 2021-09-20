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
            timer = Mathf.Cos(Time.time) + 0.2f;
            kick.color = new Color(1, 1, 1, timer);
        }
        else if (other.gameObject.tag == "check2")
        {
            timer = Mathf.Cos(Time.time) + 0.2f;
            cutter.color = new Color(1, 1, 1, timer);
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
