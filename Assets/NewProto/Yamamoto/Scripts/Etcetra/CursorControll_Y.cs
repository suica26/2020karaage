using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControll_Y : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
