using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_M : MonoBehaviour
{
    public GameObject OptionPanel;

    void Start()
    {
        //オプションパネルを消す
        OptionPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Onstart()
    {
        //オプションパネルを出す
        OptionPanel.SetActive(true);
    }
}
