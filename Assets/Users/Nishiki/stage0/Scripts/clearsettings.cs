using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clearsettings : MonoBehaviour
{
    public GameObject whiteness;
    public SceneReference nextScene;

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("OnWhiteness", 1.3f);
            Invoke("SceneChange", 5f);
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
