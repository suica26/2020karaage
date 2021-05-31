using UnityEngine;
using UnityEngine.Video;
 
public class movieclick01 : MonoBehaviour
{
	public VideoClip videoClip;
	public GameObject screen;

	public int time;
	public int stoptime01;
	public int stoptime02;
	public int stoptime03;
	public int stoptime04;
	public int stoptime05;
	public int stoptime06;
	public int stoptime07;
	public int stoptime08;
	public int stoptime09;
	public int stoptime10;
	public int stoptime11;

	public int finaltime;

	public bool count = true;

	public GameObject cursor;

	public GameObject nextscene;
	

	void Start()
	{
		var videoPlayer = screen.AddComponent<VideoPlayer>();   // videoPlayeコンポーネントの追加

		videoPlayer.source = VideoSource.VideoClip; // 動画ソースの設定
		videoPlayer.clip = videoClip;

		//videoPlayer.isLooping = true;   // ループの設定
	}

    public void FixedUpdate()
    {

		var videoPlayer = GetComponent<VideoPlayer>();

		if (count == true)
        {

			time = time + 1;
			videoPlayer.Play(); // 動画を再生する。

			cursor.SetActive(false);

		}

		if (time == stoptime01 || time == stoptime02 || time == stoptime03 || time == stoptime04 || time == stoptime05
			|| time == stoptime06 || time == stoptime07 || time == stoptime08 || time == stoptime09 || time == stoptime10 || time == stoptime11)
        {

			videoPlayer.Pause();    // 動画を一時停止する。
			count = false;

			cursor.SetActive(true);

		}

		if (count == false && Input.GetMouseButtonDown(0))
        {

			count = true;
			time = time + 1;

			videoPlayer.Play(); // 動画を再生する。

			cursor.SetActive(false);

		}

		if (count == false && Input.GetKeyDown("return"))
		{

			count = true;
			time = time + 1;

			videoPlayer.Play(); // 動画を再生する。

			cursor.SetActive(false);

		}


		if (time >= finaltime)
        {

			nextscene.SetActive(true);

		}
	}
}