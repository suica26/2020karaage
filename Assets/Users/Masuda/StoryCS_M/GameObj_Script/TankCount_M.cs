using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankCount_M : MonoBehaviour
{
    private Tank_Y tank;
    private GameObject player;
    private Mission2_M m2m;
    private Mission3_M m3m;
    private string nowScene;
    private bool tantan;

    void Start()
    {
        nowScene = SceneManager.GetActiveScene().name;
        player = GameObject.Find("Player");
        tank = this.GetComponent<Tank_Y>();
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
        if (tank.HP <= 0 && !tantan)
        {
            m3m.EnemyBreak_Tank();
            tantan = true;
        }
    }
}
