using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_M : MonoBehaviour
{
    public Slider volSlider;
    public CriAtomSource cas;
    [SerializeField] public static float vol;
    void Start()
    {
        cas = (CriAtomSource)GetComponent("CriAtomSource"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundChanger()
    {
        cas.volume = volSlider.value;
        vol = volSlider.value;
    }
}
