using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage_M : MonoBehaviour
{
    public string sceneName;
    public GameObject loading;
    private new CriAtomSource audio;
    void Start()
    {
        loading.SetActive(false);
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        audio.Play("System_Decision");
        loading.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}
