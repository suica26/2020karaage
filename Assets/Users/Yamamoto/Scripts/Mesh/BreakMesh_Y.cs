using System.Collections.Generic;
using UnityEngine;

public class BreakMesh_Y : MonoBehaviour
{
    public float breakValue = 0.1f;
    public MeshFilter[] myMesh;
    public Vector3[] vertPos;
    public Vector3[] vertPosToWorld;

    void Start()
    {
        myMesh = gameObject.GetComponentsInChildren<MeshFilter>();

        foreach (var meshes in myMesh)
        {
            vertPos = meshes.mesh.vertices;
            vertPosToWorld = vertPos;
            var vertexArray = vertPos;

            for (int i = 0; i < vertPos.Length; i++)
            {
                vertexArray[i] += new Vector3(Random.Range(-breakValue, breakValue), Random.Range(-breakValue, breakValue), Random.Range(-breakValue, breakValue));
            }

            meshes.mesh.SetVertices(vertexArray);
        }
    }
}