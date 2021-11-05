using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_M : MonoBehaviour
{
    [SerializeField] private GameObject setPanel;

    public void OnClick()
    {
        setPanel.SetActive(true);
    }
}
