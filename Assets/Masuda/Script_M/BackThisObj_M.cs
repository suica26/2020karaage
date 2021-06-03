using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackThisObj_M : MonoBehaviour
{
    [SerializeField] GameObject it, it2;
    [SerializeField] public int judge2;
    private new CriAtomSource audio;
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    public void OnClick()
    {
        audio.Play("System_Cancel");
        it.SetActive(false);
        if (judge2 == 0)
        {
            it2.SetActive(true);
        }
        else
        {
            it2.SetActive(false);
        }
    }
}
