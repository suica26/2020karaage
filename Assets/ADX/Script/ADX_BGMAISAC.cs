using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ADX_BGMAISAC : MonoBehaviour
{
    private float BGMAISAC;
    public bool EnemyBool;
    public CriAtomSource bgmCriAtomSource;//BGMのCriAtomSourceアタッチしないと効かない
    EvolutionChicken_R scrEvo;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        BGMAISAC = 0f;
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
        EnemyBool = false;
}

    // Update is called once per frame
    void Update()
    {
        //nullチェックを追加　山本
        if (scrEvo != null)
        {
            if (scrEvo.EvolutionNum == 1 && BGMAISAC < 1 && SceneManager.GetActiveScene().name == "stage1")
            {
                BGMAISAC += 0.02f;
                bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);
            }
            else if (scrEvo.EvolutionNum == 1 && SceneManager.GetActiveScene().name == "Stage2")
            {
                //BGMAISAC = 0f;
            }
            else if (scrEvo.EvolutionNum == 2 && BGMAISAC < 1 && SceneManager.GetActiveScene().name == "Stage2")
            {
                BGMAISAC += 0.02f;
                bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);
            }

            if(SceneManager.GetActiveScene().name == "Stage2")
            {
                if (EnemyBool == true && BGMAISAC <= 1)
                {
                    BGMAISAC += 0.05f;
                }
                else if (EnemyBool == false && BGMAISAC >= 0)
                {
                    BGMAISAC -= 0.05f;
                }
            }
        }
        Debug.Log(BGMAISAC);
        bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);
    }
}
