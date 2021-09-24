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
    public Text achieve;

    void Start()
    {
        player = GameObject.Find("Player");
        fireHyd = GetComponent<FireHydrant_R>();
        osmY = GetComponent<ObjectStateManagement_Y>();
        prePos = transform.position;
        m2m = player.GetComponent<Mission2_M>();
    }

    private void OnDestroy()
    {
        if (m2m != null && m2m.fourth)
        {
            prePos.y = -1000;
            m2m.gasTank += 1;
            m2m.achieve += 1;
            achieve.text = m2m.achieve + " / 3";
        }
    }
}
