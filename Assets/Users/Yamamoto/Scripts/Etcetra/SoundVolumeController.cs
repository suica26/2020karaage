using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundVolumeController : MonoBehaviour
{
    public float soundVolume = 1f;
    public GameObject[] soundmaster;
    public int loadSceneTimes;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //中断したときにSoundVolumeが複数個存在するのを防ぐ処理
        soundmaster = GameObject.FindGameObjectsWithTag("SoundVolume");
        if (soundmaster.Length > 1) Destroy(this.gameObject);
    }

    void Update()
    {
        loadSceneTimes++;
        /*
        CriAtom.SetCategoryVolume("BGM", soundVolume);
        CriAtom.SetCategoryVolume("SFX", soundVolume);
        CriAtom.SetCategoryVolume("Voice", soundVolume);
        CriAtom.SetCategoryVolume("Ambient", soundVolume);
        */
        CriAtom.SetCategoryVolume("", soundVolume);
    }
}
