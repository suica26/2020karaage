using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEnglish : MonoBehaviour
{
    [SerializeField]
    public Text text1, text2, text3, text4, text5, text6, text7,
        text8, text9;
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
        text3.text = "Adjust the volume";
        text4.text = "Quality change";
        text5.text = "Language change";
        text6.text = "Finish the game?";
        text7.text = "Low";
        text8.text = "High";
        text9.text = "Japanese";
    }
}
