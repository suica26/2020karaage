using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundVolumeController : MonoBehaviour
{
    public float soundVolume = 1f;
    public GameObject[] soundmaster;
    private string[] CatergoryNames = new string[4]{
        "BGM", "SFX", "Voice", "Ambient"
    };
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
        foreach (var category in CatergoryNames)
            CriAtom.SetCategoryVolume(category, soundVolume);
    }
}
