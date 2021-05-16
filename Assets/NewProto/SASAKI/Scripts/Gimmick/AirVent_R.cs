using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVent_R : BlowerGimmickBase
{
    [SerializeField] private float _minIntervalTime;
    [SerializeField] private float _maxIntervalTime;
    private float intervalTime;

    // Start is called before the first frame update
    void Start()
    {
        intervalTime = Random.Range(_minIntervalTime, _maxIntervalTime);
    }

    // Update is called once per frame
    void Update()
    {
        intervalTime -= Time.deltaTime;
        if (intervalTime <= 0f)
        {
            intervalTime = Random.Range(_minIntervalTime, _maxIntervalTime);
            InstanceObject();
            InstanceEffect();
        }
    }
}
