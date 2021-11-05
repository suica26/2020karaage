using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back1p_M : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel1, optionPanel2;

    public void OnClick()
    {
        optionPanel1.SetActive(true);
        optionPanel2.SetActive(false);
    }
}
