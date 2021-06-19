using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISAC_Con_Tset : MonoBehaviour
{
    private float BGM_AISACNUM;
    public CriAtomSource bgmCriAtomSource;//BGMのCriAtomSourceアタッチしないと効かない
    // Start is called before the first frame update
    void Start()
    {
        SetBGMControlValue(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Invoke("Wait", 1f);
        }
            AISACDown();
        //Debug.Log(BGM_AISACNUM);
    }
    private void Wait()
    {

        AISACUp();
    }


    //ADX　Aisac値変更
    private void SetBGMControlValue(float value)
    {
        bgmCriAtomSource.SetAisacControl("BGM_Aisac", value);
    }
    //ADX　時間経過でAisac値下降
    private void AISACDown()
    {
        if (BGM_AISACNUM >= 0.0f)
        {
            BGM_AISACNUM -= Time.deltaTime / 20f;
            SetBGMControlValue(BGM_AISACNUM);
            
        }
    }
    //ADX　Aisac値上昇
    private void AISACUp()
    {
        if (BGM_AISACNUM <= 1.0f)
        {
            //Debug.Log("AISACChange");
            BGM_AISACNUM += 0.05f;
            SetBGMControlValue(BGM_AISACNUM);
        }
    }
}
