using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscore : MonoBehaviour
{
    private Animator animCon;
    public int damage;
    public int[] breakPoints;
    public int nowanim;
    public GameObject air;
    public GameObject arrow;

    public GameObject ironeffect;

    public bool hit = false;
    //ADX
    private CriAtomSource criAtomSource;
    int time;

    // Start is called before the first frame update
    void Start()
    {
        animCon = GetComponent<Animator>();
        criAtomSource = GetComponent<CriAtomSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (nowanim < breakPoints.Length && damage >= breakPoints[nowanim])
        {
            //nowanimは加算されてから下の式に代入される
            animCon.SetInteger("bp", ++nowanim);
        }

        if (nowanim == 5)
        {
            time = time + 1;
        }

        if (time >= 50)
        {
            air.SetActive(false);
            arrow.SetActive(true);

            nowanim = 6;
            time = 0;

            criAtomSource.Play("door_broken00");
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "Cutter(Clone)")
        {
            hit = true;
            damage++;
            if (ironeffect != null)
                Instantiate(ironeffect, new Vector3(-6, 1, -15), Quaternion.identity);
            criAtomSource.Play("Track_noise00");
        }
    }
}
