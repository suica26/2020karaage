using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovieSkip_M : MonoBehaviour
{
    public GameObject skip;
    public bool dual;
    public float doubleClick, counter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            dual = true;
            counter += 1;
        }

        //二度目のクリック判定
        if(counter >= 2)
        {
            //ダブルクリックでスキップ
            skip.SetActive(true);
            ResetStates();
        }

        if (dual)
        {
            //二回のクリック間の時間計測
            doubleClick += Time.deltaTime;
        }

        if (doubleClick >= 1f)
        {
            //クリックから一秒でリセット
            ResetStates();
        }

    }

    public void ResetStates()
    {
        //クリック状況の情報をリセット
        doubleClick = 0;
        counter = 0;
        dual = false;
    }
}
