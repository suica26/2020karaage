using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovieSkip_M : MonoBehaviour
{
    public GameObject skip;
    public bool dual;
    public float doubleClick, counter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            dual = true;
            counter += 1;
        }

        if(counter >= 2)
        {
            skip.SetActive(true);
            ResetStates();
        }

        if (dual)
        {
            doubleClick += Time.deltaTime;
        }

        if (doubleClick >= 1f)
        {
            ResetStates();
        }

    }

    public void ResetStates()
    {
        doubleClick = 0;
        counter = 0;
        dual = false;
    }
}
