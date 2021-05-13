using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Clear_M : MonoBehaviour
{
    [SerializeField] public GameObject clear, company, next;
    void Start()
    {
        clear.SetActive(false);
        next.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!company)
        {
            //Time.timeScale = 0;
            clear.SetActive(true);
            next.SetActive(true);
        }
    }
}
