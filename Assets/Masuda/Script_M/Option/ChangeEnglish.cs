using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEnglish : MonoBehaviour
{
    [SerializeField]
    public Text text1, text2, text3, text4, text5, text6, text7,
        text8, text9, text10, text11;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        text1.text = "Score Attack";
        text2.text = "Story Mode";
        text3.text = "How to Play";
        text4.text = "Option";
        text5.text = "Option";
        text6.text = "Quality";
        text7.text = "Low";
        text8.text = "High";
        text9.text = "Language";
        text10.text = "Japanese";
        text11.text = "Title";
    }
}
