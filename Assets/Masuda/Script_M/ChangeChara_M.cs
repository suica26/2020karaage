using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChara_M : MonoBehaviour
{
    GameObject FirstChicken;
    GameObject SecondChicken;
    GameObject ThirdChicken;
    GameObject FinalChicken;
    public int firstEvo = 100;
    public int secondEvo = 200;
    public int thirdEvo = 300;
    public int scorePoint;

    void Start()
    {
        //全形態を読み込み
        this.FirstChicken = GameObject.Find("1stChicken");
        this.SecondChicken = GameObject.Find("2ndChicken");
        this.ThirdChicken = GameObject.Find("3rdChicken");
        this.FinalChicken = GameObject.Find("FinalChicken");
    }

    void Change()
    {
        //第1形態を最初に出現させる
        FirstChicken.SetActive(true);
        SecondChicken.SetActive(false);
        ThirdChicken.SetActive(false);
        FinalChicken.SetActive(false);
    }

    void Update()
    {
        //第1形態から第2形態に変化
        if (scorePoint >= firstEvo)
        {
            FirstChicken.SetActive(false);
            SecondChicken.SetActive(true);
        }
        //第2形態から第3形態に変化
        if (scorePoint >= secondEvo)
        {
            SecondChicken.SetActive(false);
            ThirdChicken.SetActive(true);
        }
        //第3形態から最終形態に変化
        if (scorePoint >= thirdEvo)
        {
            ThirdChicken.SetActive(false);
            FinalChicken.SetActive(true);
        }
    }
}
