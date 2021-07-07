using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_PlayDirection : MonoBehaviour
{
    private CriAtomSource Sound;
    private float span = 0.5f;
    private float currentTime = 0f;
    private GameObject MainCamera;
    public float distance = 30;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");

        Sound = GetComponent<CriAtomSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //span秒に1回だけ処理する
        currentTime += Time.deltaTime;
        if (currentTime > span)
        {
            float _distance = Vector3.Distance(MainCamera.transform.position, transform.position);
            if (_distance < distance)
            {
                Sound.Play();
            }
            else
            {
                Sound.Stop();
            }
            currentTime = 0f;
        }
    }
}

