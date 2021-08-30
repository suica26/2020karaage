using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイムスケール0の際に音をPauseするスクリプト
public class ADX_Pause : MonoBehaviour
{
    private CriAtomSource Sound;

    private void OnEnable()
    {
        Sound = GetComponent<CriAtomSource>();
        Sound.Play("Pause");
    }

    private void OnDisable()
    {
        Sound.Play("Resume");
    }
}
