using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart2_M : MonoBehaviour
{
    [SerializeField] public GameObject load;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStart()
    {
        load.SetActive(true);
        SceneManager.LoadScene("stage1");
    }

}
