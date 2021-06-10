using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_M : MonoBehaviour
{
    public Slider volSlider;
    public CriAtomSource cas;

    [SerializeField] public static float vol;
    public SoundVolumeController soundVolumeController;
    private int timer;

    // Update is called once per frame
    void Update()
    {
        timer++;
        if (timer == 5)
        {
            var soundVolumeObject = GameObject.Find("SoundVolume");
            soundVolumeController = soundVolumeObject.GetComponent<SoundVolumeController>();
            volSlider.value = soundVolumeController.soundVolume;
        }

        if (timer > 6)
        {
            vol = volSlider.value;
            if (soundVolumeController == null)
            {
                CriAtom.SetCategoryVolume("BGM", vol);
                CriAtom.SetCategoryVolume("SFX", vol);
                CriAtom.SetCategoryVolume("Voice", vol);
                CriAtom.SetCategoryVolume("Ambient", vol);
            }
            else
            {
                soundVolumeController.soundVolume = vol;
            }
        }
    }

    /*
    //ADX　Aisac値変更
    private void VolumeControlValue(float value)
    {
        cas.SetAisacControl("Volume", value);
    }
    */
}
