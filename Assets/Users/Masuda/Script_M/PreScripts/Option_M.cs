using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_M : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel1, pausePanel2;

    void Start()
    {
        pausePanel1.SetActive(false);
        pausePanel2.SetActive(false);

    }

    public void OnClick()
    {
        pausePanel1.SetActive(true);
    }
}
