using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] public static bool japanese;
    private Stage1_Mission_M s1M;
    [SerializeField] public static int je;

    void Start()
    {
        japanese = true;
        s1M = GetComponent<Stage1_Mission_M>();
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        if (je == 0)
        {
            japanese = true;
            Debug.Log("jap");
        }
        else
        {
            japanese = false;
            Debug.Log("eigo");//
        }
        
    }

    public static bool getLanguage()
    {
        return japanese;//
    }
}
