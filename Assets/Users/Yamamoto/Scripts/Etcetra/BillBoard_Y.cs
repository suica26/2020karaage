using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard_Y : MonoBehaviour
{
    private Transform cameraPos;
    // Update is called once per frame
    private void Start()
    {
        cameraPos = GameObject.FindWithTag("MainCamera").transform;
    }

    void Update()
    {
        transform.LookAt(cameraPos);
    }
}
