using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryShop_Y : MonoBehaviour
{
    // Start is called before the first frame update

    private ObjectStateManagement_Y objScr;
    private int time;
    public int deathFrameTimeng;
    void Start()
    {
        if (ScoreAttack_Y.gameMode == mode.ScoreAttack) gameObject.SetActive(false);
        objScr = GetComponent<ObjectStateManagement_Y>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time == deathFrameTimeng)
        {
            Debug.Log("Death Shop");
            objScr.HP = 0;
            objScr.Damage(0, 1);
        }

        time++;
    }
}
