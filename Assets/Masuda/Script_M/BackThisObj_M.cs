using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackThisObj_M : MonoBehaviour
{
    [SerializeField] GameObject it,it2;
    [SerializeField] public int judge2;
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        it.SetActive(false);
        if (judge2 == 0)
        {
            it2.SetActive(true);
        }
        else
        {
            it2.SetActive(false);
        }
    }
}
