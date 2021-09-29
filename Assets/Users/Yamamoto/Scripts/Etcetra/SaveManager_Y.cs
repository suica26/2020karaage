using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct SaveData
{
    public bool[] stageFlg;
    public bool[] stageClearFlg;
    public float soundVolume;
    public int quality;
    public string language;
    public float cameraSensitive;
}

public class SaveManager_Y : MonoBehaviour
{
    public static SaveData sd;
    const string SAVE_FILE_PATH = "save.json";
    private bool isSaving = false;
    private bool isLoading = false;
    static private SaveManager_Y instance;
    private bool isMobile;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        Load();
        QualitySettings.SetQualityLevel(sd.quality);
    }

    public void Save()
    {
        if (isSaving || isLoading) return;
        isSaving = true;
        StartCoroutine(FlgReset());

        string json = JsonUtility.ToJson(sd);
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory();
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
        path += ("/" + SAVE_FILE_PATH);
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
    }

    public void Load()
    {
        if (isLoading || isSaving) return;
        isLoading = true;
        StartCoroutine(FlgReset());

        try
        {
#if UNITY_EDITOR
            string path = Directory.GetCurrentDirectory();
#else
            string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
            FileInfo info = new FileInfo(path + "/" + SAVE_FILE_PATH);
            StreamReader reader = new StreamReader(info.OpenRead());
            string json = reader.ReadToEnd();
            sd = JsonUtility.FromJson<SaveData>(json);
        }
        catch (Exception e)
        {
            sd = new SaveData();
            sd.stageFlg = new bool[3] { false, false, false };
            sd.stageClearFlg = new bool[3] { false, false, false };
            sd.soundVolume = 0.7f;
            sd.language = "Japanese";
            sd.quality = 2;
            sd.cameraSensitive = 1f;
        }
    }

    public IEnumerator FlgReset()
    {
        yield return new WaitForSeconds(0.3f);
        isSaving = false;
        isLoading = false;
    }
    public void SaveStageFlg(int stageNum)
    {
        sd.stageFlg[stageNum - 1] = true;
        Save();
    }

    public bool GetStageFlg(int stageNum)
    {
        return sd.stageFlg[stageNum - 1];
    }

    public void SaveClearFlg(int stageNum)
    {
        sd.stageClearFlg[stageNum - 1] = true;
        Save();
    }

    public bool GetClearFlg(int stageNum)
    {
        return sd.stageClearFlg[stageNum - 1];
    }

    public void SaveSoundVolume(float vol)
    {
        sd.soundVolume = vol;
        Save();
    }

    public float GetSoundVolume()
    {
        return sd.soundVolume;
    }

    public void SaveLanguage(string lang)
    {
        sd.language = lang;
        Save();
    }

    public string GetLanguage()
    {
        return sd.language;
    }

    public void SaveQuality(int num)
    {
        sd.quality = num;
        Save();
    }

    public void SaveCameraSensitive(float sensitive)
    {
        sd.cameraSensitive = sensitive;
        Save();
    }

    public float GetCameraSensitive()
    {
        return sd.cameraSensitive;
    }
}
