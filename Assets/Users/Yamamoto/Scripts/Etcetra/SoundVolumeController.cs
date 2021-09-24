using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundVolumeController : MonoBehaviour
{
    public float currentVolume = 0f;
    public float nowVolume = 0f;
    public GameObject[] soundmaster;
    private string[] CatergoryNames = new string[4]{
        "BGM", "SFX", "Voice", "Ambient"
    };
    private SaveManager_Y saveManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //中断したときにSoundVolumeが複数個存在するのを防ぐ処理
        soundmaster = GameObject.FindGameObjectsWithTag("SoundVolume");
        if (soundmaster.Length > 1) Destroy(this.gameObject);
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager_Y>();
        if (saveManager != null)
            currentVolume = saveManager.GetSoundVolume();
    }

    void Update()
    {
        foreach (var category in CatergoryNames)
            CriAtom.SetCategoryVolume(category, currentVolume);
        if (currentVolume != nowVolume)
        {
            nowVolume = currentVolume;
            if (saveManager != null)
                saveManager.SaveSoundVolume(nowVolume);
        }
    }
}
