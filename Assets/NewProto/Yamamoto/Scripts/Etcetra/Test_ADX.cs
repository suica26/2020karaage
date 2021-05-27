using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ADX : MonoBehaviour
{
    private CriAtomSource criAtomSource;
    private string cueName;

    // Update is called once per frame
    private void Start()
    {
        criAtomSource = GetComponent<CriAtomSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
            cueName = "BuildingExplosion00";
        }

        if (Input.GetMouseButton(0))
        {
            criAtomSource.Play(cueName);
        }
    }
}
