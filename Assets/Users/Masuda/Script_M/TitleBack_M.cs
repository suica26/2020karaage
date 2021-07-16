using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBack_M : MonoBehaviour
{
    public GameObject clearPanel;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            clearPanel.SetActive(true);
            Cursor.visible = true;
        }
    }

    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
