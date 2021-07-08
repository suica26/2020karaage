using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOff_M : MonoBehaviour
{
    public void OnClick()
    {
        //山本修正 プラットフォーム依存コンパイルに対応させました
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
