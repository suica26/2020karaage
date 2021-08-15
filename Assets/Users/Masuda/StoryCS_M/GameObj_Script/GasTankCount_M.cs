using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasTankCount_M : MonoBehaviour
{
    private Mission2_M m2m;
    private ObjectStateManagement_Y osmY;
    public GameObject player;
    public Vector3 pos,prePos;
    public bool drop;
    private int tankHP;
    public Text achieve;
        
    void Start()
    {
        player = GameObject.Find("Player");
        m2m = player.GetComponent<Mission2_M>();
        osmY = this.gameObject.GetComponent<ObjectStateManagement_Y>();
        prePos = this.gameObject.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.gameObject.gameObject.transform.position;
        tankHP = osmY.HP;
        if (tankHP == 0 && m2m.third)
        {
            m2m.gasTank += 1;
            m2m.achieve += 1;
            achieve.text = m2m.achieve + " / 3";
        }

        else if (pos.y <= prePos.y-5 && m2m.third)
        {
            prePos.y = -1000;
            m2m.gasTank += 1;
            m2m.achieve += 1;
            achieve.text = m2m.achieve + " / 3";
        }

    }
}
