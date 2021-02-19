using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_option : MonoBehaviour
{
    [SerializeField] private GameObject backButton, optionPanel;

    void Start()
    {
        optionPanel.SetActive(false);
    }


    void Update()
    {

    }

    public void OnClick()
    {
        optionPanel.SetActive(false);
    }
}
