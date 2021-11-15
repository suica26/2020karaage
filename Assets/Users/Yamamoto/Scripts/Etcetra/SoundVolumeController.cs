using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundVolumeController : MonoBehaviour
{
    public float currentVolume = 0f;
    public float nowVolume = 0f;
    private string[] CatergoryNames = new string[5]{
        "BGM", "SFX", "Voice", "Ambient", "Action"
    };
    private SaveManager_Y saveManager;
    static private SoundVolumeController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
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
