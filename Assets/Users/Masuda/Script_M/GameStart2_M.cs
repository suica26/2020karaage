using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart2_M : MonoBehaviour
{
    [SerializeField] public GameObject load,logo;
    public string sceneName;

    public void OnStart()
    {
        load.SetActive(true);
        logo.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }
}
