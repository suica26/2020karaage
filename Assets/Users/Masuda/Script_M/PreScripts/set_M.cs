using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_M : MonoBehaviour
{
    [SerializeField] private GameObject setPanel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        setPanel.SetActive(true);
    }
}
