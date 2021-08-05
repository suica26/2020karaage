using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-103)]
public class BuildDynamicNavMesh : MonoBehaviour
{
    private NavMeshSurface _surface;
    // Start is called before the first frame update
    void Start()
    {
        _surface = GetComponent<NavMeshSurface>();
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while (true)
        {
            _surface.BuildNavMesh();

            yield return new WaitForSeconds(5.0f);
        }
    }
}
