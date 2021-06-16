using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manhole_R : BlowerGimmickBase
{
    private new CriAtomSource audio;
    private GameObject _nullCheckObj;

    private void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }
    private void OnTriggerEnter(Collider other)
    {
        //マンホールのギミック        
        if (other.gameObject.name == "fallAttackCircle(Clone)"  && _nullCheckObj == null)       
        {
            _nullCheckObj = InstanceObject();
            InstanceEffect();
            audio.Play("Manhole00");
        }
    }
}
