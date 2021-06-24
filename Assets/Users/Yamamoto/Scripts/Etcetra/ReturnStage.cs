using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnStage : MonoBehaviour
{
    private GameObject player;

    /// <summary>
    /// 鶏の座標がreturnYを過ぎたら復帰する
    /// </summary>
    public Vector3 returnAreas;
    private Vector3 P;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        P = player.transform.position;
        if ((P.x > returnAreas.x || P.x < -returnAreas.x) ||
            (P.z > returnAreas.x || P.z < -returnAreas.x) ||
            P.y < returnAreas.y)
            player.transform.position = transform.position;
    }
}
