using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_SurroundPos : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        this.transform.position = pos;
    }
}
