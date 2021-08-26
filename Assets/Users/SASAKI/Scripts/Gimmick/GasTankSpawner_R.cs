using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTankSpawner_R : MonoBehaviour
{
    [SerializeField] private GameObject tank;
    [SerializeField] private GameObject[] objs;
    private float timer = 10.0f;

    private float[] timers;

    private void Start()
    {
        timers = new float[objs.Length];

        for (int i = 0; i < timers.Length; i++)
            timers[i] = timer;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < objs.Length; i++)
        {
            if(objs[i].transform.childCount == 0)
            {
                timers[i] -= Time.deltaTime;
                if(timers[i] <= 0f)
                {
                    var instance = Instantiate(tank);
                    instance.transform.position = objs[i].transform.position;
                    instance.transform.rotation = objs[i].transform.rotation;
                    Destroy(objs[i]);

                    timers[i] = timer;
                    objs[i] = instance;
                }
            }
        }
    }
}
