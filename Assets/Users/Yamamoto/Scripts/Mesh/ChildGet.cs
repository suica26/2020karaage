using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildGet : MonoBehaviour
{
    public GameObject[] children;

    // Start is called before the first frame update
    void Start()
    {
        children = new GameObject[transform.childCount];

        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                children[i] = transform.GetChild(i).gameObject;
            }
        }
    }
}
