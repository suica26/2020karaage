using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOff_M : MonoBehaviour
{
    public GameObject thisObj;
    //private new CriAtomSource audio;
    public Mission1_M mm1;

    void Start()
    {
        //audio = (CriAtomSource)GetComponent<CriAtomSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        //audio.Play("System_Cancel");
        thisObj.SetActive(false);
        mm1.hipStamp = false;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
