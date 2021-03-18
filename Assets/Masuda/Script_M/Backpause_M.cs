using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpause_M : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
