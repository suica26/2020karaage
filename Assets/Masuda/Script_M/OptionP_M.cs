using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionP_M : MonoBehaviour
{
    [SerializeField] private GameObject setPanelP;
    private new CriAtomSource audio;
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        audio.Play("System_Decision");
        setPanelP.SetActive(true);
    }

}
