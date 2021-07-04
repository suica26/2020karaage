using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LisnerPos : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Player;
    private float m, n;
    // Start is called before the first frame update
    void Start()
    {
        //Camera:Player = m:nに内分
        m = 9; n = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Apos = Camera.transform.position;
        Vector3 Bpos = Player.transform.position;

        Vector3 center = (n * Camera.transform.position + m * Player.transform.position) / (m + n);

        this .transform.position = center;

    }
}
