using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TimerMovie_M : MonoBehaviour
{
    [SerializeField] public VideoClip timer;
    [SerializeField] public GameObject timerWipe;
    public VideoPlayer vp;
    void Start()
    {
        //たぶん動画再生できる？パクリ
        vp = timerWipe.AddComponent<VideoPlayer>();
        vp.source = VideoSource.VideoClip;
        vp.clip = timer;

        vp.isLooping = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
