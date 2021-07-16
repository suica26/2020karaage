using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wireframe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        
        Shader shader = Shader.Find("Unlit/wireframe");
        gameObject.GetComponent<Renderer>().material.shader = shader;
        
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh.SetIndices(meshFilter.mesh.GetIndices(0), MeshTopology.Lines, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
