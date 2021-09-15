using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPauseOption_M : MonoBehaviour
{
    [SerializeField] private GameObject pauseOption, starts;
    private new CriAtomSource audio;
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    public void OnClick()
    {
        audio.Play("System_Cancel");
        pauseOption.SetActive(false);
        starts.SetActive(true);
    }
}
