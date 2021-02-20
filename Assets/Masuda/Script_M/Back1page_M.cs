using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back1page_M : MonoBehaviour
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
        optionPanel.SetActive(true);
        optionPanel2.SetActive(false);
    }
}
