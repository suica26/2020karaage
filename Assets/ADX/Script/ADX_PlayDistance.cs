using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_PlayDistance : MonoBehaviour
{
    private CriAtomSource Sound;
    private GameObject player;
    public float distance = 20;
    float _distance;
    private float span = 0.1f;
    private float currentTime = 0f;
    public string cueName;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        Sound = GetComponent<CriAtomSource>();

        span = Random.Range(0.1f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        //span秒に1回だけ処理する
        currentTime += Time.deltaTime;
        if (currentTime > span)
        {
            _distance = Vector3.Distance(player.transform.position, transform.position);
            if (_distance < distance)
            {
                PlayAndStopSound();
            }
            else
            {
                Sound.Stop();
            }
            //Debug.Log(_distance);
            //Debug.Log(Sound.status);
            currentTime = 0f;
        }
    }

    //再生状況監視
    private void PlayAndStopSound()
    {
        if ((Sound.status == CriAtomSource.Status.Stop) || (Sound.status == CriAtomSource.Status.PlayEnd))
        {
            Sound.Play(cueName);
        }
    }
}

