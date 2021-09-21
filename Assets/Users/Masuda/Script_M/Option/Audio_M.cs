using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_M : MonoBehaviour
{
    public Slider volSlider;
    public CriAtomSource cas;

    [SerializeField] public static float vol = 0f;
    public SoundVolumeController soundVolumeController;
    private int timer;
    private bool isUpdate;

    private void Start()
    {
        isUpdate = false;
        Invoke("SetInstance", 0.1f);
    }

    private void SetInstance()
    {
        var soundVolumeObject = GameObject.Find("SoundVolume");
        soundVolumeController = soundVolumeObject.GetComponent<SoundVolumeController>();
        volSlider.value = soundVolumeController.nowVolume;
        isUpdate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUpdate)
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
                soundVolumeController.currentVolume = vol;
            }
        }
    }
}
