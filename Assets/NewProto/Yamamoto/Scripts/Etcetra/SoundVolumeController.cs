using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundVolumeController : MonoBehaviour
{
    public float soundVolume = 1f;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        CriAtom.SetCategoryVolume("BGM", soundVolume);
        CriAtom.SetCategoryVolume("SFX", soundVolume);
        CriAtom.SetCategoryVolume("Voice", soundVolume);
        CriAtom.SetCategoryVolume("Ambient", soundVolume);
    }
}
