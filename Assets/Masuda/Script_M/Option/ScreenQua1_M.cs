using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenQua1_M : MonoBehaviour
{
    private new CriAtomSource audio;
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        QualitySettings.SetQualityLevel(0);
        audio.Play("System_Decision");
    }
}
