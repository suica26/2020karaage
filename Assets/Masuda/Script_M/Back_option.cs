using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_option : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel, optionPanel2;

    void Start()
    {
       
    }


    void Update()
    {

    }

    public void OnClick()
    {
        optionPanel.SetActive(false);
        optionPanel2.SetActive(false);
    }
}
