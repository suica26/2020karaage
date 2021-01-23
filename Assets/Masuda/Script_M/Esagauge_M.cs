using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Esagauge_M : MonoBehaviour
{
    int maxEP = 100;
    int currentEP = 0;
    int getEP = 10;

    public Slider slider;
    public GameObject Food;

    void Start()
    {
        //えさ用のスライダーの大きさを0に
        slider.value = 0;
        slider.maxValue = maxEP;
    }

   　private void OnTriggerEnter(Collider collider)
    {
        //えさに当たったらEPが加算されていき、ゲージが増える
        if (collider.gameObject == Food)
        {
            currentEP = currentEP + getEP;
            slider.value = currentEP;
        }  
    }

    void FixedUpdate()
    { 
        //ゲージが満タンになったら最初の状態に戻す
        if (currentEP == maxEP)
        {
            slider.value = 0;
        }

    }
}
