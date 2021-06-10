using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_SoundPlay01 : MonoBehaviour
{
    private CriAtomSource criAtomSource;
    // Start is called before the first frame update
    void Start()
    {
        criAtomSource = GetComponent<CriAtomSource>();
    }
    void OnEnable()
    {
        criAtomSource.Play("JINGLE_Mission");
    }
}
