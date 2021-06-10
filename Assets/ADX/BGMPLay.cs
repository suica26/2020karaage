using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPLay : MonoBehaviour
{
    private CriAtomSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM.GetComponent<CriAtomSource>();

    }

    // Update is called once per frame
    void Update()
    {
        BGM.Play("BGM00");
    }
}
