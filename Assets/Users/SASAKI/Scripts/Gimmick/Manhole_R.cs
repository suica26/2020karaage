using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manhole_R : BlowerGimmickBase
{
    private new CriAtomSource audio;
    private GameObject _nullCheckObj;
    //M
    private Mission1_M m1m;
    public GameObject player;

    private void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
        //M
        player = GameObject.Find("Player");
        m1m = player.GetComponent<Mission1_M>();
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
            if (m1m.fourth)
            {
                m1m.manhole += 1;
                m1m.achieve += 1;
                m1m.per.text = m1m.achieve + "/ 3";
            }
        }
    }
}
