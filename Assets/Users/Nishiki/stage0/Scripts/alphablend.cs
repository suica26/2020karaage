using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alphablend : MonoBehaviour
{

    MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material.color = mr.material.color - new Color32(0, 0, 0, 255);
    }
}
