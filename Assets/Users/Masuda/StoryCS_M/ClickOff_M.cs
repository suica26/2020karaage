using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOff_M : MonoBehaviour
{
    [SerializeField] public GameObject thisObj;
    [SerializeField] bool breaker;
    private new CriAtomSource audio;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        audio.Play("System_Cancel");
        Time.timeScale = 1f;
        thisObj.SetActive(false);
        breaker = false;
    }
}
