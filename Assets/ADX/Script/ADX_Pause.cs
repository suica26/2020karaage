using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイムスケール0の際に音をPauseするスクリプト
public class ADX_Pause : MonoBehaviour
{
    private CriAtomSource Sound;

    private void Start()
    {
        Sound = GetComponent<CriAtomSource>();
    }

    private void OnEnable()
    {
        Sound?.Play("Pause");
    }

    private void OnDisable()
    {
        Sound?.Play("Resume");
    }
}
