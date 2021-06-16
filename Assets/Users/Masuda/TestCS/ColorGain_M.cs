using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGain_M : MonoBehaviour
{
    [SerializeField] private GameObject score;
    float a_color = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.GetComponent<TextMesh>().color = new Color(0, 0, 0, a_color);
        a_color -= Time.deltaTime; 
    }
}
