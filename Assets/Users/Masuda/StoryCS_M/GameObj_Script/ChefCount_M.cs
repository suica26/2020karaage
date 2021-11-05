using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChefCount_M : MonoBehaviour
{
    private Chef_Y chef;
    private GameObject player;
    private Mission2_M m2m;
    private Mission3_M m3m;
    private string nowScene;
    private bool cock;

    void Start()
    {
        nowScene = SceneManager.GetActiveScene().name;
        player = GameObject.Find("Player");
        chef = this.GetComponent<Chef_Y>();
        if (nowScene == "Stage2")
        {
            m2m = player.GetComponent<Mission2_M>();
            m3m = null;
        }
        else if (nowScene == "Stage3")
        {
            m2m = null;
            m3m = player.GetComponent<Mission3_M>();
        }
    }

    void Update()
    {
        if (chef.HP <= 0 && !cock)
        {
            if (nowScene == "Stage2")
            {
                m2m.EnemyBreak();
                cock = true;

            }
            else if (nowScene == "Stage3")
            {
                m3m.EnemyBreak_Chef();
                cock = true;
            }
        }
    }
}