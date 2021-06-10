using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_M : MonoBehaviour
{
    public Slider volSlider;
    public CriAtomSource cas;

    [SerializeField] public static float vol;
    private SoundVolumeController soundVolumeController;

    void Start()
    {
        var soundVolumeObject = GameObject.Find("SoundVolume");
        soundVolumeController = soundVolumeObject.GetComponent<SoundVolumeController>();
    }

    // Update is called once per frame
    void Update()
    {
        vol = volSlider.value;
        soundVolumeController.soundVolume = vol;
        CriAtom.SetCategoryVolume("BGM", vol);
        CriAtom.SetCategoryVolume("SFX", vol);
        CriAtom.SetCategoryVolume("Voice", vol);
        CriAtom.SetCategoryVolume("Ambient", vol);
    }

    /*
    //ADX　Aisac値変更
    private void VolumeControlValue(float value)
    {
        cas.SetAisacControl("Volume", value);
    }
    */
}
