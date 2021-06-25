using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_AISAC_Col : MonoBehaviour
{

    [Header("BGMのCriAtomSourceアタッチ")]
    public CriAtomSource bgmCriAtomSource;//BGMのCriAtomSourceアタッチしないと効かない
    public float BGM_AISACNUM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit"); // ログを表示する
    }
}
