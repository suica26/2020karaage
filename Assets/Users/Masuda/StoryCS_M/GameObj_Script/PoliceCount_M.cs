using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoliceCount_M : MonoBehaviour
{
    private Police_Y police;
    private GameObject player;
    private Mission2_M m2m;
    private Mission3_M m3m;
    private string nowScene;
    private bool safety;

    void Start()
    {
        nowScene = SceneManager.GetActiveScene().name;
        player = GameObject.Find("Player");
        police = this.GetComponent<Police_Y>();
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

    // Update is called once per frame
    void Update()
    {
        if (police.HP <= 0 && !safety)
        {
            if (nowScene == "Stage2")
            {
                m2m.EnemyBreak();
                safety = true;
            }
            else if (nowScene == "Stage3")
            {
                m3m.EnemyBreak_Police();
                safety = true;
            }
        }
    }
}
