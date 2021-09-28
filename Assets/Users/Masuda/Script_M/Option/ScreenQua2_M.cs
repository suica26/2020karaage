using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenQua2_M : MonoBehaviour
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
        QualitySettings.SetQualityLevel(2);
        if (saveManager != null)
            saveManager.SaveQuality(2);
        criAtomSource.Play("System_Decision");
    }
}
