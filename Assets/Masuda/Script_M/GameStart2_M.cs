using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart2_M : MonoBehaviour
{
    [SerializeField] public GameObject option;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStart()
    {
        SceneManager.LoadScene("stage1");
    }

}
