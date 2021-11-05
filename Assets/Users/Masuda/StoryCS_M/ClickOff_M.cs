using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOff_M : MonoBehaviour
{
    public GameObject thisObj;
    //private new CriAtomSource audio;
    public Mission1_M mm1;

    public void OnClick()
    {
        thisObj.SetActive(false);
        mm1.hipStamp = false;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
