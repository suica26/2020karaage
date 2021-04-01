using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICon_test_M : MonoBehaviour
{
    [SerializeField] private Text addScore;
    float gain;

    void Start()
    {
        gain = 0;
        addScore.color = new Color(0, 0, 0, 0);
    }
   
    public void ColorUI()
    {
        gain = Time.deltaTime;
        addScore.color = new Color(0, 0, 0, 1 - gain); 
    }

    void Update()
    {
        
    }
}
