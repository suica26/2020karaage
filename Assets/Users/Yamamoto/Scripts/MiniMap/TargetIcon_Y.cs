using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIcon_Y : MiniMapIcon_Y
{
    public float radius;
    private GameObject player;
    public GameObject triangle;
    public GameObject sphere;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        var p = player.transform.position;
        var dir = target.transform.position - p;
        dir.y = 0f;
        if (dir.magnitude <= radius)
        {
            var t = target.transform.position;
            transform.position = new Vector3(t.x, y, t.z);
            triangle.SetActive(false);
            sphere.SetActive(true);
        }
        else
        {
            dir = dir.normalized * radius;
            transform.position = new Vector3(p.x + dir.x, y, p.z + dir.z);
            transform.forward = dir;
            triangle.SetActive(true);
            sphere.SetActive(false);
        }
    }
}
