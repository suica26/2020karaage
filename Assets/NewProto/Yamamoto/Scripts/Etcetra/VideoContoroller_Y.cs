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
    public double time;

    private void Start()
    {
        videoPlayer.Play();
        movingVideo = true;
    }

    private void Update()
    {
        time = videoPlayer.time;

        if (videoPlayer.time >= timings[nextCutTiming] && movingVideo)
        {
            StopCut();

            if (nextCutTiming >= timings.Length - 1) nextscene.SetActive(true);
        }

        if (!movingVideo && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("return")))
        {
            NextCutStart();
        }
        else if (movingVideo && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("return")))
        {
            SkipCurrentCut();
        }
    }

    private void StopCut()
    {
        videoPlayer.Pause();
        movingVideo = false;
        cursor.SetActive(true);
    }

    private void NextCutStart()
    {
        movingVideo = true;
        videoPlayer.time = timings[nextCutTiming];  //一応、再生位置の補正をかける
        nextCutTiming++;
        videoPlayer.Play();
        cursor.SetActive(false);
    }

    private void SkipCurrentCut()
    {
        videoPlayer.time = timings[nextCutTiming];
    }
}
