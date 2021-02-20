using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextOption_M : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel, optionPanel2;

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        optionPanel.SetActive(false);
        optionPanel2.SetActive(true);
    }
}
