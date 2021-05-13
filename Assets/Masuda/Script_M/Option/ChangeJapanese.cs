using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeJapanese : MonoBehaviour
{
    [SerializeField]
    public Text text1, text2, text3, text4, text5, text6, text7,
        text8, text9, text10, text11;
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
        text3.text = "操作方法";
        text4.text = "設定";
        text5.text = "設定";
        text6.text = "画質";
        text7.text = "低い";
        text8.text = "高い";
        text9.text = "言語";
        text10.text = "日本語";
        text11.text = "タイトルへ";
    }
}
