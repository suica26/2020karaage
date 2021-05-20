using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeJapanese : MonoBehaviour
{
    [SerializeField]
    public Text text1, text2, text3, text4, text5, text6, text7,
        text8, text9;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        text1.text = "スコアアタック";
        text2.text = "ストーリーモード";
        text3.text = "音量の調節";
        text4.text = "画質の変更";
        text5.text = "言語の変更";
        text6.text = "ゲームを終了しますか？";
        text7.text = "低い";
        text8.text = "高い";
        text9.text = "日本語";
    }
}
