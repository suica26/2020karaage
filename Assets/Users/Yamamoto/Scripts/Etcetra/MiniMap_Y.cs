using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap_Y : MonoBehaviour
{
    public GameObject player;
    public float y;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, y, player.transform.position.z);
    }
}
