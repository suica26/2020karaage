using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos_M : MonoBehaviour
{
    //ニワトリを追跡するカメラ
    //targetにニワトリを入れる
    public Transform target;
    public float smoothing = 5.0f;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            targetCamPos,
            Time.deltaTime * smoothing
        );
    }
}
