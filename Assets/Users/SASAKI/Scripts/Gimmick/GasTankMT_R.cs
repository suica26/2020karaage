using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTankMT_R : MonoBehaviour
{
    private List<GameObject> buildings;

    public int CollisionCount { get { return buildings.Count; } }

    private float timer;

    void Start()
    {
        timer = 0;
        buildings = new List<GameObject>();
    }

    void Update()
    {
        if (transform.parent.GetComponent<ObjectStateManagement_Y>().HP == 0)
            Destroy(transform.parent.gameObject);

        if(CollisionCount >= 5)
        {
            transform.parent.GetComponent<ObjectStateManagement_Y>().HP = 0;
        }

        if(!transform.parent.GetComponent<Rigidbody>().isKinematic)
        {
            timer += Time.deltaTime;
            if (timer >= 2.0 && transform.parent.GetComponent<Rigidbody>().velocity.magnitude < 2.0)
            {
                transform.parent.GetComponent<ObjectStateManagement_Y>().HP = 0;
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 15)
            return;

        foreach(var obj in buildings)
        {
            if(other == obj)
            {
                return;
            }
        }
        buildings.Add(other.gameObject);
        Debug.Log(buildings.Count);
    }
}
