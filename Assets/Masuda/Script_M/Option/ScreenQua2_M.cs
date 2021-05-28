using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenQua2_M : MonoBehaviour
{
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
        QualitySettings.SetQualityLevel(2);
        audio.Play("System_Decision");
    }
}
