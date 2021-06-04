using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Next_M : MonoBehaviour
{
    [SerializeField] GameObject panel;
    void Start()
    {
        panel.SetActive(false);
    }

    public void OnClick()
    {
        panel.SetActive(true);
    }
}
