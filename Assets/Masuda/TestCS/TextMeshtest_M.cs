using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshtest_M : MonoBehaviour
{
    [SerializeField] private GameObject score, obj;
    [SerializeField] private int price;
    string strSCORE;
    float a_color = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.transform.rotation = Camera.main.transform.rotation;
        strSCORE = price.ToString();
        score.GetComponent<TextMesh>().text = "+$ " + strSCORE;
        if (obj == false)
        {
            score.GetComponent<TextMesh>().color = new Color (0, 0, 0, a_color);
            a_color -= Time.deltaTime;
        }
        if (a_color < 0)
        {
            a_color = 0;
        }
    }
}
