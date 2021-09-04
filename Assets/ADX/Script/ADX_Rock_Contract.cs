using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_Rock_Contract : MonoBehaviour
{
    private  CriAtomSource ContractSound;
    private ADX_SoundRaycast ADX_RevLevel_L, ADX_RevLevel_R;
    private string Rev_L,Rev_R; //ADXバス名
    public GameObject player { get; private set; }
    private bool isCalledOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ContractSound = GetComponent<CriAtomSource>();
        //ADX_RevLevel_L = player.GetComponent<ADX_Ray_Rev>();
        //ADX_RevLevel_R = player.GetComponent<ADX_Ray_Rev>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCalledOnce)
        {
            //float BusLevel_L = ADX_RevLevel_L.ADX_BusSendLevel_L;
            //float BusLevel_R = ADX_RevLevel_R.ADX_BusSendLevel_R;

            //SetBusSendLevelSet(Rev_L, BusLevel_L); Debug.Log("L" + BusLevel_L);
            //SetBusSendLevelSet(Rev_R, BusLevel_R); Debug.Log("R" + BusLevel_R);
            isCalledOnce = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        ContractSound.Play();

    }
    //バスセンド量調整メソッド
    private void SetBusSendLevelSet(string busName, float levelOffset)
    {
        ContractSound.SetBusSendLevelOffset(busName, levelOffset);
    }
}
