using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_GasRoll : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CriAtomSource Sound;
    public float Check　= 1f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        Sound = GetComponent<CriAtomSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.magnitude > Check)
        {
            Sound.Play("GasRolling00");
        }
    }
}
