using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ADX_BGMAISAC : MonoBehaviour
{
    private float BGMAISAC, span, BGMEvo;
    public CriAtomSource bgmCriAtomSource;//BGMのCriAtomSourceアタッチしないと効かない
    EvolutionChicken_R scrEvo;
    private GameObject player;
    private bool battleFlg = false;
    private List<GameObject> fightingEnemies = new List<GameObject>();
    public bool St3Fase = false;

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

        //デフォルト形態セレクターセット
        bgmCriAtomSource.player.SetSelectorLabel("Chicken_Form", "form1");
    }

    // Update is called once per frame
    void Update()
    {
        //nullチェックを追加　山本
        if (scrEvo != null)
        {
            //St1設定
            if (SceneManager.GetActiveScene().name == "stage1")
            {
                if (scrEvo.EvolutionNum == 1 && BGMEvo < 1f)
                {
                    BGMEvo += 0.015f;
                }
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

                if (scrEvo.EvolutionNum == 2 && BGMEvo < 1f)
                {
                    BGMEvo += 0.015f;
                }
            }

            // St3設定
            if (SceneManager.GetActiveScene().name == "Stage3")
            {
                //フェーズ戦BGM切り替え
                if (St3Fase && BGMAISAC < 1)
                {
                    BGMAISAC += 0.015f;
                }
                if (scrEvo.EvolutionNum == 3 && BGMEvo < 1f)
                {
                    BGMEvo += 0.015f;
                }

            }
        }
        //Debug.Log(BGMAISAC);
        bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);
        bgmCriAtomSource.SetAisacControl("BGMEvo", BGMEvo);
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
