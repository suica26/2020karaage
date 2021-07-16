using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISAC_Con_Tset : MonoBehaviour
{
    private float BGMAISAC;
    public CriAtomSource bgmCriAtomSource;//BGMのCriAtomSourceアタッチしないと効かない
    EvolutionChicken_R scrEvo;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        scrEvo = player.GetComponent<EvolutionChicken_R>();
        BGMAISAC  = 0f;
        bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);

    }

    // Update is called once per frame
    void Update()
    {
        if (scrEvo.EvolutionNum == 1 && BGMAISAC < 1)
        {
            BGMAISAC += 0.02f;
            bgmCriAtomSource.SetAisacControl("BGM_Aisac", BGMAISAC);
            Debug.Log("EvoSpund");
        }
    }


}
