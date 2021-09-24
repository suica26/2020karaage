using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ADX_BGMAISAC : MonoBehaviour
{
    private float BGMAISAC, span;
    public CriAtomSource bgmCriAtomSource;//BGMのCriAtomSourceアタッチしないと効かない
    EvolutionChicken_R scrEvo;
    private GameObject player;
    private bool battleFlg = false;
    private List<GameObject> fightingEnemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        BGMAISAC = 0f; span = 3f;
        bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);

        string bgmName = "";
        switch (SceneManager.GetActiveScene().name)
        {
            case "stage0": bgmName = "BGM_St0"; break;
            case "stage1": bgmName = "BGM01"; break;
            case "Stage2": bgmName = "BGM02"; break;
            case "Stage3": bgmName = "BGM03"; break;
            default: bgmName = "BGM00"; break;
        }
        bgmCriAtomSource.Play(bgmName);

        player = GameObject.Find("Player");
        if (player != null)
            scrEvo = player.GetComponent<EvolutionChicken_R>();

        StartCoroutine("Logging");
    }

    // Update is called once per frame
    void Update()
    {
        //nullチェックを追加　山本
        if (scrEvo != null)
        {
            //St1設定
            if (scrEvo.EvolutionNum == 1 && BGMAISAC < 1 && SceneManager.GetActiveScene().name == "stage1")
            {
                BGMAISAC += 0.02f;
                bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);
            }


            //St2設定
            if (SceneManager.GetActiveScene().name == "Stage2")
            {
                //接敵
                if (battleFlg)
                {
                    bool clearFlg = true;
                    foreach (var e in fightingEnemies)
                    {
                        if (e != null)
                        {
                            clearFlg = false;
                            break;
                        }
                    }
                    if (clearFlg) battleFlg = false;


                    BGMAISAC += 0.015f;
                    if (BGMAISAC > 1f) BGMAISAC = 1f;
                    //Debug.Log(BGMAISAC);
                }
                else if (BGMAISAC > 0f)
                {
                    BGMAISAC -= 0.02f;
                    if (BGMAISAC < 0f) BGMAISAC = 0f;
                }
                //第3形態
                if (scrEvo.EvolutionNum == 2)
                {

                }
            }
        }



        //Debug.Log(BGMAISAC);
        bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);
    }

    public void SetBattleBGM(GameObject enemy)
    {
        //すでにリストに追加済みの敵だったら無視
        foreach (var e in fightingEnemies) if (e == enemy) return;
        fightingEnemies.Add(enemy);

        if (battleFlg) return;

        fightingEnemies.Clear();
        fightingEnemies.Add(enemy);
        battleFlg = true;
    }

    IEnumerator Logging()
    {
        while (true)
        {
            yield return new WaitForSeconds(span);
            fightingEnemies.Clear();
        }
    }
}
