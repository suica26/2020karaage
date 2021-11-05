using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBack_M : MonoBehaviour
{
    public GameObject clearPanel;
    public string sceneName;

    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
