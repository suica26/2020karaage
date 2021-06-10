using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_SoundPlay01 : MonoBehaviour
{
    private new CriAtomSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }
    void OnEnable()
    {
        audio.Play("JINGLE_Mission");
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
