using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTestCon : MonoBehaviour
{
    private CriAtomSource Sound;
    // Start is called before the first frame update
    void Start()
    {
        Sound = GetComponent<CriAtomSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Sound.Play();
            
    }
}
