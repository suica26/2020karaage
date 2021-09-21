using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenQua1_M : MonoBehaviour
{
    private CriAtomSource criAtomSource;
    private SaveManager_Y saveManager;
    void Start()
    {
        criAtomSource = GetComponent<CriAtomSource>();
        if (saveManager != null)
            saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager_Y>();
    }

    void OnClick()
    {
        //一応mediumに変更
        QualitySettings.SetQualityLevel(1);
        if (saveManager != null)
            saveManager.SaveQuality(1);
        criAtomSource.Play("System_Decision");
    }
}
