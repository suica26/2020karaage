using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ciclegauge_M : MonoBehaviour
{
    [SerializeField] private Image circle;
    public float a;
    bool gainPower;

    void Start()
    {
        gainPower = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            circle.fillAmount += a;
        }

        if (circle.fillAmount >= 1.0f)
        {
            gainPower = true;
            GainCircle();
        }
        /*if (circle.fillAmount == 0f)
        {
            gainPower = false;
        }
        */
    }

    void GainCircle()
    {
        circle.fillAmount = 0f;
        /*if (gainPower)
        {
            circle.fillAmount -= Time.deltaTime;
        }
        */
    }
}
