using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_M : MonoBehaviour
{
    public Slider volSlider;

    [SerializeField] public static float vol = 0f;
    private SoundVolumeController soundVolumeController;
    private string[] CatergoryNames = new string[4]{
        "BGM", "SFX", "Voice", "Ambient"
    };

    private void Start()
    {
        var soundVolumeObject = GameObject.Find("SoundVolume");
        soundVolumeController = soundVolumeObject.GetComponent<SoundVolumeController>();
        volSlider.value = soundVolumeController.nowVolume;
    }

    // Update is called once per frame
    void Update()
    {
        vol = volSlider.value;
        if (soundVolumeController == null)
        {
            foreach (var category in CatergoryNames)
                CriAtom.SetCategoryVolume(category, vol);
        }
        else
        {
            soundVolumeController.currentVolume = vol;
        }
    }
}
