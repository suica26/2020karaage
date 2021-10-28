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
    private static SaveManager_Y saveManager;
    private static bool countDown = true;
    public static NCMBObject[] ranking;
    public static NCMBObject[] neighbors;
    private static int currentRank;

    private void Awake()
    {
        if (instance == null) instance = this;
        return;
    }

    private void Start()
    {
        saveManager = SaveManager_Y.GetInstance();
        Init();
    }

    private void Update()
    {
        if (gameMode == mode.ScoreAttack && !countDown)
        {
            sunsetTimer += Time.deltaTime;
            if (sunsetTimer >= 0.5f) SunsetChange();

            if (evoMatTimer > 0f) evoMatTimer -= Time.deltaTime;
            else if (evoMatTimer <= 0f) evoMatTimer = 0f;

            limitTime -= Time.deltaTime;
            if (limitTime <= 0f)
            {
                FinishScoreAttack();
            }
        }
    }

    public static void Init()
    {
        limitTime = MAXLIMITTIME;
        score = 0;
        countDown = true;
    }

    private void FinishScoreAttack()
    {
        limitTime = 0f;
        gameMode = mode.Result;
    }

    public bool CheckNewRecord(int stageNum)
    {
        if (saveManager.GetlocalScore(stageNum) > score) return true;
        else return false;
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

    public static void SubmitScore(string name, int stageNum)
    {
        // クラスのNCMBObjectを作成
        NCMBObject obj = null;
        switch (stageNum)
        {
            case 1: obj = new NCMBObject("St1"); break;
            case 2: obj = new NCMBObject("St2"); break;
            case 3: obj = new NCMBObject("St3"); break;
            default: break;
        }

        if (obj != null)
        {
            // オブジェクトに値を設定
            obj["name"] = name;
            obj["score"] = 400;
            // データストアへの登録
            obj.SaveAsync((NCMBException e) =>
            {
                if (e != null) Debug.Log($"保存に失敗しました。エラーコード{e.ErrorCode}");
                else
                {
                    Debug.Log($"保存に成功しました。objectID = {obj.ObjectId}");
                    saveManager.SaveNCMBID(stageNum, obj.ObjectId);
                }
            });
        }
        else
        {
            Debug.Log("ステージ番号が不正です。1,2,3のいずれかから選んでください");
        }
    }

    private static void FetchRank(int currentScore, int stageNum)
    {
        // データスコアの「HighScore」から検索
        NCMBQuery<NCMBObject> rankQuery = null;
        switch (stageNum)
        {
            case 1: rankQuery = new NCMBQuery<NCMBObject>("St1"); break;
            case 2: rankQuery = new NCMBQuery<NCMBObject>("St2"); break;
            case 3: rankQuery = new NCMBQuery<NCMBObject>("St3"); break;
            default: break;
        }
        rankQuery.WhereGreaterThan("score", currentScore);
        rankQuery.CountAsync((int count, NCMBException e) =>
        {
            //件数取得失敗
            if (e != null) Debug.Log($"ランキング件数の取得に失敗しました。エラーコード{e.ErrorCode}");
            else
            {
                //件数取得成功
                Debug.Log($"ランキング件数の取得に成功しました。");
                currentRank = count + 1; // 自分よりスコアが上の人がn人いたら自分はn+1位
            }
        });
    }

    public static void GetWorldTopScore(int stageNum)
    {
        NCMBQuery<NCMBObject> query = null;
        switch (stageNum)
        {
            case 1: query = new NCMBQuery<NCMBObject>("St1"); break;
            case 2: query = new NCMBQuery<NCMBObject>("St2"); break;
            case 3: query = new NCMBQuery<NCMBObject>("St3"); break;
            default: break;
        }

        //scoreフィールドの降順でデータを取得
        query.OrderByDescending("score");

        //検索件数を8件に設定
        query.Limit = 8;

        //データストアでの検索を行う
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null) Debug.Log($"検索に失敗しました。エラーコード{e.ErrorCode}");
            else
            {
                Debug.Log($"検索に成功しました。");
                ranking = objList.ToArray();
            }
        });
    }

    public static void GetAroundMyScores(int currentScore, int stageNum)
    {
        FetchRank(currentScore, stageNum);
        // スキップする数を決める（ただし自分が1位か2位のときは調整する）
        int numSkip = currentRank - 3;
        if (numSkip < 0) numSkip = 0;

        // データストアから検索
        NCMBQuery<NCMBObject> query = null;
        switch (stageNum)
        {
            case 1: query = new NCMBQuery<NCMBObject>("St1"); break;
            case 2: query = new NCMBQuery<NCMBObject>("St2"); break;
            case 3: query = new NCMBQuery<NCMBObject>("St3"); break;
            default: break;
        }
        query.OrderByDescending("score");
        query.Skip = numSkip;
        query.Limit = 5;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null) Debug.Log($"周辺ランキングの検索に失敗しました。エラーコード{e.ErrorCode}");
            else
            {
                Debug.Log($"周辺ランキングの検索に成功しました。");
                neighbors = objList.ToArray();
            }
        });
    }
}
