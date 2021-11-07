using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCancel_M : MonoBehaviour
{
    //ゲームを中断してタイトル画面に戻る
    public void OnClick()
    {
        SceneManager.LoadScene("GameStart_M");
    }
}
