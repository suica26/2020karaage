using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControll_Y : MonoBehaviour
{
    public bool gameMode;

    // Start is called before the first frame update
    void Start()
    {
        if (gameMode)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
