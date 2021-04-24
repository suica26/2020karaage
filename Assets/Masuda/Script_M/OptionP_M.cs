using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionP_M : MonoBehaviour
{
    [SerializeField] private GameObject setPanelP;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        setPanelP.SetActive(true);
    }

}
