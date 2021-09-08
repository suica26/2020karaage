using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovieSkip_M : MonoBehaviour
{
    public GameObject skip;
    public bool twin, dual;
    public float doubleClick;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            doubleClick += Time.deltaTime;
            dual = true;
        }

        if (doubleClick < 1f)
        {
            if (Input.GetMouseButton(0))
            {
                twin = true;
            }
        }

        if (twin)
        {
            twin = false;
            dual = false;
            skip.SetActive(true);
            doubleClick = 0;
        }

        if (doubleClick >= 1f)
        {
            doubleClick = 0;
            twin = false;
            dual = false;
        }

    }
}
