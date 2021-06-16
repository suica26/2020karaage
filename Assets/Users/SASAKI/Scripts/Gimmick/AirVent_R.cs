using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVent_R : BlowerGimmickBase
{
    [SerializeField] private float _minIntervalTime;
    [SerializeField] private float _maxIntervalTime;
    private float intervalTime;


    void Start()
    {
        intervalTime = Random.Range(_minIntervalTime, _maxIntervalTime);
    }

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
