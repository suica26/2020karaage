using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaker : MonoBehaviour
{

    public CameraShake shake;

    public float duration;
    public float magnitude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shake.Shake(duration, magnitude);
    }
}
