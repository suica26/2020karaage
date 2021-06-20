using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manhole_R : BlowerGimmickBase
{
    private new CriAtomSource audio;
    private GameObject _nullCheckObj;
    //M
    private Stage1_Mission_M s1mm;
    public GameObject player;

    private void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
        //M
        player = GameObject.Find("Player");
        s1mm = player.GetComponent<Stage1_Mission_M>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //マンホールのギミック        
        if (other.gameObject.name == "fallAttackCircle(Clone)"  && _nullCheckObj == null)       
        {
            _nullCheckObj = InstanceObject();
            InstanceEffect();
            audio.Play("Manhole00");
            //M
            s1mm.manhole += 1;
        }
    }
}
