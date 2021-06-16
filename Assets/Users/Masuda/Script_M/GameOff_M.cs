using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOff_M : MonoBehaviour
{
    public void OnClick()
    {
        //山本修正 UnityEditorの機能を使ったものはビルド後のexeでは機能しなくなってしまうみたいなので、変更しました
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
