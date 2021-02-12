using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Esapoint_M : MonoBehaviour
{
    int maxEP = 100;
    int currentEP = 0;
    int getEP = 10;

    [SerializeField] public Slider epSlider;
    [SerializeField] public GameObject Food;

    void Start()
    {
        //えさ用のスライダーの大きさを0に
        epSlider.value = 0;
        epSlider.maxValue = maxEP;
    }

    private void OnTriggerEnter(Collider collider)
    {
        //えさに当たったらEPが加算されていき、ゲージが増える
        if (collider.gameObject == Food)
        {
            currentEP = currentEP + getEP;
            epSlider.value = currentEP;
        }
    }

    void FixedUpdate()
    {
        //ゲージが満タンになったら最初の状態に戻す
        if (currentEP == maxEP)
        {
            epSlider.value = 0;
            currentEP = 0;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            currentEP += getEP;
            epSlider.value = currentEP;
        }
    }
}
