using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearsettings : MonoBehaviour
{
    public GameObject whiteness;
    public GameObject Schanger;

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("OnWhiteness", 1.3f);
            Invoke("OnSceneChanger", 5f);
        }
    }

    private void OnWhiteness()
    {
        whiteness.SetActive(true);
    }
    private void OnSceneChanger()
    {
        Schanger.SetActive(true);
    }
}
