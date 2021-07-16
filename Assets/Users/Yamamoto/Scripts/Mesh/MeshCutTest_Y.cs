using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BLINDED_AM_ME;

public class MeshCutTest_Y : MonoBehaviour
{
    public GameObject victim;
    public GameObject blade;
    public Material capMaterial;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MeshCut.Cut(victim, blade.transform.position, blade.transform.forward, capMaterial);
        }
    }
}
