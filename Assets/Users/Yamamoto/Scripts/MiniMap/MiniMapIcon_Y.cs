using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon_Y : MonoBehaviour
{
    public GameObject target;
    public float y;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(target.transform.position.x, y, target.transform.position.z);
    }
}
