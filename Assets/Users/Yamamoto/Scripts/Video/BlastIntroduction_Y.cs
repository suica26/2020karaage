using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BlastIntroduction_Y : MonoBehaviour
{
    public GameObject topObject, mission, subMission, exMission;
    private Text mis, subMis, exMis;
    public Animation misAnim;
    public float introStart;
    public VideoPlayer movie;
    public bool isIntro, isBlast, blasted;
    public float blastCharge;
    public double videoTime;
    private SaveManager_Y saveManager;
    public bool isJapanese;
    private CriAtomSource Sound;
    // Start is called before the first frame update
    void Start()
    {
        mis = mission.GetComponent<Text>();
        subMis = subMission.GetComponent<Text>();
        exMis = exMission.GetComponent<Text>();
        blastCharge = 0f;
        isIntro = isBlast = blasted = false;

        var saveObj = GameObject.Find("SaveManager");
        if (saveObj != null)
            saveManager = saveObj.GetComponent<SaveManager_Y>();
        if (saveManager == null || saveManager.GetLanguage() == "Japanese") isJapanese = true;
        else if (saveManager.GetLanguage() == "English") isJapanese = false;

        Sound = GetComponent<CriAtomSource>();
    }

    // Update is called once per frame
    void Update()
    {
        videoTime = movie.time;

        if (!blasted)
        {
            if (movie.time >= introStart && !isIntro) Intro_Start();
            if (isIntro && !isBlast)
            {
                if (Input.GetMouseButton(2))
                {
                    Sound.Play("MovieBlast1");
                    blastCharge += Time.deltaTime;
                }
                else
                {
                    Sound.Stop();
                    blastCharge = 0f;
                }
                if (blastCharge >= 3f) Intro_LetsBlast();

            }
            if (isBlast && Input.GetMouseButtonUp(2))
            {
                movie.Play();
                blasted = true;
                topObject.SetActive(false);
                Sound.Play("MovieBlast2");
            }
        }
    }

    private void Intro_Start()
    {
        topObject.SetActive(true);
        isIntro = true;
        movie.Pause();
        misAnim.Play();

        if (isJapanese)
        {
            mis.text = "おはようブラストを使ってみよう！";
            subMis.text = "おはようブラストをチャージ！";
            exMis.text = "マウスホイールを長押ししよう！";
        }
        else
        {
            mis.text = "Let's use Morning Blast!";
            subMis.text = "Charge Morning Blast!";
            exMis.text = "Press Mouse wheel!";
        }
    }

    private void Intro_LetsBlast()
    {
        isBlast = true;
        misAnim.Play();

        if (isJapanese)
        {
            mis.text = "おはようブラストを使ってみよう！";
            subMis.text = "おはようブラストを発射！";
            exMis.text = "マウスホイールを離そう！";
        }
        else
        {
            mis.text = "Let's use Morning Blast!";
            subMis.text = "Play Morning Blast!";
            exMis.text = "Release Mouse wheel!";
        }
    }
}
