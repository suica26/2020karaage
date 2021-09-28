using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOff_M : MonoBehaviour
{
    //山本修正　ゲーム終了時にセーブするように変更
    private SaveManager_Y saveManager;
    private void Start()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager_Y>();
    }

    public void OnClick()
    {
        saveManager.Save();
        //山本修正 プラットフォーム依存コンパイルに対応させました
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
