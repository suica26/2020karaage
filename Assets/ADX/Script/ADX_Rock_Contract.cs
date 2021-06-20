using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_Rock_Contract : MonoBehaviour
{
    private  CriAtomSource ContractSound;
    // Start is called before the first frame update
    void Start()
    {
        ContractSound = GetComponent<CriAtomSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        ContractSound.Play();

    }
}
