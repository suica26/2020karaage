using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstButToSecondBut_M : MonoBehaviour
{
    [SerializeField] public GameObject firstButtons, nexts;
    private new CriAtomSource audio;
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    public void OnClick()
    {
        audio.Play("System_Decision");
        firstButtons.SetActive(false);
        nexts.SetActive(true);
    }
}
