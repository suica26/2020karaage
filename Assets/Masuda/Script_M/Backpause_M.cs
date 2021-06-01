using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpause_M : MonoBehaviour
{
    private new CriAtomSource audio;
    [SerializeField] private GameObject pausePanel;
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }
    public void OnClick()
    {
        audio.Play("System_Cancel");
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
