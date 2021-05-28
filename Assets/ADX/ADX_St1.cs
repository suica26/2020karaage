using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_St1 : MonoBehaviour
{
    public new CriAtomSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
        audio.player.SetSelectorLabel("Selector_Floor", "wood");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
