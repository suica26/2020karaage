using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIcon_M : MonoBehaviour
{
    [SerializeField] GameObject skip;
    public string sceneName;
    void Start()
    {
        skip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (skip)
        {
            Time.timeScale = 0;
        }
        else if (!skip)
        {
            Time.timeScale = 1;
        }
    }

    public void OnSkip()
    {
        skip.SetActive(true);
    }

    public void OffSkip()
    {
        skip.SetActive(false);
    }

    public void SkipStory()
    {
        SceneManager.LoadScene(sceneName);
    }
}
