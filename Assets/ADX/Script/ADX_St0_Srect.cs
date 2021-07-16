using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_St0_Srect : MonoBehaviour
{
    public CriAtomSource audioLavel;
    // Start is called before the first frame update
    void Start()
    {
        audioLavel = (CriAtomSource)GetComponent("CriAtomSource");
    }
    void Update()
    {
        audioLavel.player.SetSelectorLabel("Chicken_Form", "St0");
        audioLavel.player.SetSelectorLabel("Selector_Floor", "wood");
    }
}
