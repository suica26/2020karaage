using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public sealed class ScoreAttack_Y : MonoBehaviour
{
    public static ScoreAttack_Y instance { get; private set; }
    private const float MAXLIMITTIME = 180;
    public static float limitTime { get; private set; }
    public static int score { get; private set; }
    public enum mode
    {
        Story,
        ScoreAttack,
        Result
    }
    public static mode gameMode;
    public static GameObject directionalLight;
    [SerializeField] private Material[] skyboxes;
    private float sunsetTimer;
    private static float evoMatTimer;

    private void Awake()
    {
        if (instance == null) instance = this;
        return;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (gameMode == mode.ScoreAttack)
        {
            sunsetTimer += Time.deltaTime;
            if (sunsetTimer >= 0.5f) SunsetChange();

            if (evoMatTimer > 0f) evoMatTimer -= Time.deltaTime;
            else if (evoMatTimer <= 0f) evoMatTimer = 0f;

            limitTime -= Time.deltaTime;
            if (limitTime <= 0f)
            {
                limitTime = 0f;
                gameMode = mode.Result;
            }
        }

    }

    public static void Init()
    {
        limitTime = MAXLIMITTIME;
        score = 0;
    }

    private void FinishScoreAttack()
    {

    }

    public static void AddLimitTime()
    {
        limitTime += 20;
    }

    public static void AddScore(int addScore)
    {
        score += addScore;
    }

    public static bool CheckScoreMode()
    {
        if (gameMode == mode.ScoreAttack) return true;
        else return false;
    }

    private void SunsetChange()
    {
        sunsetTimer = 0f;

        //太陽光の向き変更
        if (directionalLight != null) directionalLight.transform.eulerAngles = new Vector3(360 - limitTime * 2, 0, 0);

        //スカイボックスの変更
        if (skyboxes != null)
            RenderSettings.skybox = skyboxes[(int)(limitTime / (MAXLIMITTIME / skyboxes.Length))];
    }

    public static void SetEvolutionMaintainTimer()
    {
        evoMatTimer = 45f;
    }
}
