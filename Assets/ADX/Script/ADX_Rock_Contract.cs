using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_Rock_Contract : MonoBehaviour
{
    private  CriAtomSource ContractSound;
    private ADX_Ray_Rev ADX_RevLevel;
    private string Rev; //ADXバス名
    public GameObject player { get; private set; }
    private bool isCalledOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ContractSound = GetComponent<CriAtomSource>();
        ADX_RevLevel = player.GetComponent<ADX_Ray_Rev>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCalledOnce)
        {
            float BusLevel = ADX_RevLevel.ADX_BusSendLevel;
            SetBusSendLevelSet(Rev, BusLevel); Debug.Log("Send" + BusLevel);
            isCalledOnce = true;
        }
    }

    void OnCollisionStay(Collision other)
    {
        ContractSound.Play();

    }
    //バスセンド量調整メソッド
    private void SetBusSendLevelSet(string busName, float levelOffset)
    {
        ContractSound.SetBusSendLevelOffset(busName, levelOffset);
    }
}
