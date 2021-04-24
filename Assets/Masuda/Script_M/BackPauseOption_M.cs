using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPauseOption_M : MonoBehaviour
{
    [SerializeField] private GameObject pauseOption;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        pauseOption.SetActive(false);
    }
}
