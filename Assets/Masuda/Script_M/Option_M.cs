using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_M : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel, optionPanel2;

    void Start()
    {
        optionPanel.SetActive(false);
        optionPanel2.SetActive(false);
    }

    void Update()
    {

    }

    public void OnClick()
    {
        optionPanel.SetActive(true);
    }
}
