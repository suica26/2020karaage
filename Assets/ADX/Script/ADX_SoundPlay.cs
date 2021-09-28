using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_SoundPlay : MonoBehaviour
{
    private new CriAtomSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<CriAtomSource>();

    }
    void OnEnable()
    {
        audio?.Play();
    }

}
