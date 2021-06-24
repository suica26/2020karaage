using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMeshFilters_Y : MonoBehaviour
{
    public MeshFilter[] meshFilters;
    // Start is called before the first frame update
    void Start()
    {
        meshFilters = GetComponentsInChildren<MeshFilter>();
        foreach (var meshes in meshFilters)
        {
            Debug.Log(meshes);
        }
    }
}
