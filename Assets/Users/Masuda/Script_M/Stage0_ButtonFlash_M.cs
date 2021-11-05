using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage0_ButtonFlash_M : MonoBehaviour
{
    public Image kick, cutter;
    public float timer;
    public bool sincos;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "check1")
        {
            if (timer >= 1.0f)
            {
                sincos = true;
            }
            else if (timer <= 0)
            {
                sincos = false;
            }

            if (sincos)
            {
                timer -= 0.03f;
            }
            else if (!sincos)
            {
                timer += 0.03f;
            }

            kick.color = new Color(255, 255, 255, timer);
        }
        else if (other.gameObject.tag == "check2")
        {
            if (timer >= 1.0f)
            {
                sincos = true;
            }
            else if (timer <= 0)
            {
                sincos = false;
            }

            if (sincos)
            {
                timer -= 0.03f;
            }
            else if (!sincos)
            {
                timer += 0.03f;
            }

            cutter.color = new Color(1, 1, 1, timer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "check1")
        {
            kick.color = new Color(1, 1, 1, 0.51f);
            timer = 0;
        }
        else if (other.gameObject.tag == "check2")
        {
            cutter.color = new Color(1, 1, 1, 0.51f);
            timer = 0;
        }
    }
}
