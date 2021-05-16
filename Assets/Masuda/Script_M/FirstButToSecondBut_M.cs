using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstButToSecondBut_M : MonoBehaviour
{
    [SerializeField] public GameObject firstButtons, nexts;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        firstButtons.SetActive(false);
        nexts.SetActive(true);
    }
}
