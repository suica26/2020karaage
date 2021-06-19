using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_FootStepsControl : MonoBehaviour
{
    public new CriAtomSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            audio.player.SetSelectorLabel("Selector_Floor", "soil");

        }
        else if (other.gameObject.tag == "Wood")
        {
            audio.player.SetSelectorLabel("Selector_Floor", "wood");
        }
        else
        {
            //audio.player.SetSelectorLabel("Selector_Floor", "asphalt");
        }
    }
}
