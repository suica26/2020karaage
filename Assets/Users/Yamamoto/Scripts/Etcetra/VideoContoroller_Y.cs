using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoContoroller_Y : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject cursor;
    public GameObject nextscene;
    public double[] timings;
    private int nextCutTiming;
    private bool movingVideo;
    public double videoTime;
    public double soundTime;

    //ADX
    public int SoundCount;
    private CriAtomSource ProlSound;

    void Start()
    {
        videoPlayer.Play();
        movingVideo = true;
        Cursor.visible = true;//M
        ProlSound = (CriAtomSource)GetComponent("CriAtomSource");
    }

    private void Update()
    {
        videoTime = videoPlayer.time;

        if (videoPlayer.time >= timings[nextCutTiming] && movingVideo)
        {
            StopCut();

            if (nextCutTiming >= timings.Length - 1)
            {
                nextscene.SetActive(true);

                ProlSound.Play(SoundCount);
            }
        }

        if (!movingVideo && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("return")))
        {
            NextCutStart();
        }
        else if (movingVideo && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("return")))
        {
            SkipCurrentCut();
        }
        Debug.Log(SoundCount);
    }

    private void StopCut()
    {
        videoPlayer.Pause();        //停止
        movingVideo = false;
        cursor.SetActive(true);
    }

    private void NextCutStart()
    {
        movingVideo = true;
        videoPlayer.time = timings[nextCutTiming];  //一応、再生位置の補正をかける
        nextCutTiming++;
        videoPlayer.Play();             //再生
        cursor.SetActive(false);

        ProlSound.Play(SoundCount); //音再生
        SoundCount++;  //キューIDを増やす
    }

    private void SkipCurrentCut()
    {
        videoPlayer.time = timings[nextCutTiming];
        //SoundCount++;  //キューIDを増やす
        //ProlSound.Play(SoundCount);
    }
}
