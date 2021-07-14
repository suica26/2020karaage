using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clearsettings : MonoBehaviour
{
    public GameObject whiteness;
    public SceneReference nextScene;

    public float whiteOutTime;
    public float sceneChangeTime;

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("OnWhiteness", whiteOutTime);
            Invoke("SceneChange", sceneChangeTime);
        }
    }

    private void OnWhiteness()
    {
        whiteness.SetActive(true);
    }
    private void SceneChange()
    {
        // シーン遷移
        SceneManager.LoadScene(nextScene);
    }
}
