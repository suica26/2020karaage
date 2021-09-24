using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Y : MonoBehaviour
{
    public Vector3[] patrollRoute;
    // Start is called before the first frame update
    void Start()
    {
        patrollRoute = new Vector3[transform.childCount];
        int count = 0;
        foreach (Transform t in transform)
        {
            patrollRoute[count] = t.position;
            count++;
        }
    }
}
