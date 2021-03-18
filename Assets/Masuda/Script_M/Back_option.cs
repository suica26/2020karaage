using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_option : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel1, optionPanel2, setPanel;

    void Start()
    {
        
    }


    void Update()
    {

    }

    public void OnClick()
    {
        optionPanel1.SetActive(false);
        optionPanel2.SetActive(false);
        setPanel.SetActive(false);
    }
}
