using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause_M : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(!pausePanel.activeSelf);

            //　ポーズUIが表示されてる時は停止
            if (pausePanel.activeSelf)
            {
                Time.timeScale = 0f;
            }
            //　ポーズUIが表示されてなければ通常通り進行
            else
            {
                Time.timeScale = 1f;
            }
        }
        
    }
}
