using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISAC_Hpass : MonoBehaviour
{
    private float HpassNum;
    public CriAtomSource atomSource00, atomSource01,atomSource02;
    // Start is called before the first frame update
    void Start()
    {
        //atomSource = gameObject.GetComponent<CriAtomSource>();
    }
    void OnEnable()
    {
        HpassNum = 1f;
        atomSource00.SetAisacControl("Hpas", HpassNum);
        atomSource01.SetAisacControl("Hpas", HpassNum);
        atomSource02.SetAisacControl("Hpas", HpassNum);
    }
    private void OnDisable()
    {
        HpassNum = 0f;
        atomSource00.SetAisacControl("Hpas", HpassNum);
        atomSource01.SetAisacControl("Hpas", HpassNum);
        atomSource02.SetAisacControl("Hpas", HpassNum);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
