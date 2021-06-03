using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Clear_M : MonoBehaviour
{
    [SerializeField] public GameObject clear, company, next, pause;
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
            Destroy(pause);
            Cursor.visible = true;
            clear.SetActive(true);
            next.SetActive(true);
        }

        //テスト用、本当は!company
        if (Input.GetKeyDown(KeyCode.O))
        {
            //Time.timeScale = 0;
            Destroy(pause);
            Cursor.visible = true;
            clear.SetActive(true);
            next.SetActive(true);
        }
    }
}
