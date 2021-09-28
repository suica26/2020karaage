using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScoreMode_M : MonoBehaviour
{
    public GameObject lockLabel;
    public string states;

    void Start()
    {
        states = PlayerPrefs.GetString("storyClear");

        if (states == "clear")
        {
            lockLabel.SetActive(false);
        }
        else
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
