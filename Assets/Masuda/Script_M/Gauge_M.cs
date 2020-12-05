using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge_M : MonoBehaviour
{
    //仮で作ったゲージ
    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    
    void Update()
    {
        //Spaceを押すとゲージがたまっていく
        if (Input.GetKey(KeyCode.Space))
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x + 2.5f, 25.0f);
            if (rt.sizeDelta.x >= 150)
            {
                //たぶん1秒でマックスまで行く
                rt.sizeDelta = new Vector2(150.0f, 25.0f);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Spaceを離すとゲージが空になる
            rt.sizeDelta = new Vector2(0.0f, 50.0f);
        }
    }
}
