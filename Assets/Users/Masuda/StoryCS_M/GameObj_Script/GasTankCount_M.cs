using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasTankCount_M : MonoBehaviour
{
    private Mission2_M m2m;
    private ObjectStateManagement_Y osmY;
    private FireHydrant_R fireHyd;
    public GameObject player;
    public Vector3 pos, prePos;
    public bool comp;
    public Text achieve;

    void Start()
    {
        player = GameObject.Find("Player");
        m2m = player.GetComponent<Mission2_M>();
        fireHyd = GetComponent<FireHydrant_R>();
        osmY = GetComponent<ObjectStateManagement_Y>();
        prePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;

        if (pos.y <= prePos.y - 5 && m2m.third && !comp)
        {
            prePos.y = -1000;
            m2m.gasTank += 1;
            m2m.achieve += 1;
            achieve.text = m2m.achieve + " / 3";
            comp = true;
        }

        if (fireHyd.makeHydrant && !comp)
        {
            prePos.y = -1000;
            m2m.gasTank += 1;
            m2m.achieve += 1;
            achieve.text = m2m.achieve + " / 3";
            comp = true;
        }

        if (!this.gameObject && !comp)
        {
            prePos.y = -1000;
            m2m.gasTank += 1;
            m2m.achieve += 1;
            achieve.text = m2m.achieve + " / 3";
            comp = true;
        }
    }
}
