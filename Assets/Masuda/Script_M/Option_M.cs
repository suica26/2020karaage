using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_M : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {

    }

    public void OnClick()
    {
        pausePanel.SetActive(true);
    }
}
