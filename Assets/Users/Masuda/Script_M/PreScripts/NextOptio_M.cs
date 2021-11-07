using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextOptio_M : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel1, optionPanel2;

    public void OnClick()
    {
        //optionPanel1.SetActive(false);
        optionPanel2.SetActive(true);
    }
}
