using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LisnerPos : MonoBehaviour
{
    public GameObject objA;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Apos = objA.transform.position;
        Vector3 Bpos = Player.transform.position;

        Vector3 center = (objA.transform.position + Player.transform.position) * 0.5f;

        this .transform.position = center;

    }
}
