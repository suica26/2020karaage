using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.SceneManagement;

public sealed class ScoreAttack_Y : MonoBehaviour
{
    public static ScoreAttack_Y instance { get; private set; }
    private const float MAXLIMITTIME = 10;
    public static float limitTime { get; private set; }
    public static int score { get; private set; }
    public static mode gameMode;
    public static Parameters_R paramScr;
    public static GameObject directionalLight;
    public static Light d_light;
    private static float evoMatTimer;
    private static SaveManager_Y saveManager;
    public static bool countDown = true;
    public static NCMBObject[] topRanking;
    public static NCMBObject[] neighbors;
    private static int currentRank;
    public static int playStageNum;
    public static bool connecting;
    public static EvolutionChicken_R evoScr;
    [SerializeField] private AnimationCurve curveLC_R, curveLC_G, curveLC_B, curveLightInt;
    [SerializeField] private AnimationCurve curveR, curveG, curveB;
    [SerializeField, Range(0f, 1f)] private float lightDegree;
    private Material skyboxMat;
    private static float accel = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        saveManager = SaveManager_Y.GetInstance();
        Init();
        connecting = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Debug.Log($"gameMode:{gameMode},countDown:{countDown},limitTime:{limitTime},Score:{score}");

        if (gameMode == mode.ScoreAttack && !countDown)
        {
            SunsetChange();

            if (evoMatTimer > 0f) evoMatTimer -= Time.deltaTime;
            else if (evoMatTimer < 0f) DegenerateChicken();

            limitTime -= Time.deltaTime * accel;
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
        connecting = false;
        Debug.Log("Initialize");
    }

    /// <summary>
    /// 遊んでいるステージからステージナンバーを取得
    /// </summary>
    public static void SetPlayStageNum()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "stage1": playStageNum = 1; break;
            case "Stage2": playStageNum = 2; break;
            case "Stage3": playStageNum = 3; break;
            default: break;
        }
    }
    /// <summary>
    /// ステージ番号を入力すると設定できる
    /// </summary>
    public static void SetPlayStageNum(int stageNum)
    {
        playStageNum = stageNum;
    }

    private void FinishScoreAttack()
    {
        limitTime = 0f;
        gameMode = mode.Result;
    }

    public static bool CheckNewRecord(int stageNum)
    {
        if (saveManager.GetlocalScore(stageNum) < score)
        {
            saveManager.SavelocalScore(stageNum, score);
            return true;
        }
        else return false;
    }

    public static void AddLimitTime()
    {
        limitTime += 20;
        if (limitTime > MAXLIMITTIME) limitTime = MAXLIMITTIME;
    }

    public static void AddScore(int addScore)
    {
        score += addScore;
        if (paramScr != null) paramScr.ScoreUpdate();
    }

    public static bool CheckScoreMode()
    {
        if (gameMode == mode.ScoreAttack) return true;
        else return false;
    }

    private void SunsetChange()
    {
        float time = 1 - limitTime / MAXLIMITTIME;

        //太陽光の向き変更
        if (directionalLight != null)
        {
            //回転角を90°を中心にlightDegree以内になるように修正をかける
            var degree = ((limitTime / MAXLIMITTIME * 180) - 90) * lightDegree;
            directionalLight.transform.eulerAngles = new Vector3(90 + degree, 0f, 0f);
            d_light.intensity = curveLightInt.Evaluate(time);
            d_light.color = new Color(curveLC_R.Evaluate(time), curveLC_G.Evaluate(time), curveLC_B.Evaluate(time));
        }

        //スカイボックスの変更
        if (skyboxMat == null)
        {
            skyboxMat = new Material(RenderSettings.skybox);
            skyboxMat.SetColor("_Tint", new Color(curveR.Evaluate(time), curveG.Evaluate(time), curveB.Evaluate(time)));
            RenderSettings.skybox = skyboxMat;
        }
    }

    public static void SetEvolutionMaintainTimer()
    {
        evoMatTimer = 45f;
    }

    //ニワトリ退化
    private static void DegenerateChicken()
    {
        evoMatTimer = 0f;
        evoScr.Degenerate();
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

        connecting = true;
        if (obj != null)
        {
            // オブジェクトに値を設定
            obj["name"] = name;
            obj["score"] = score;
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

    public static void FetchRank(int currentScore, int stageNum)
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
        connecting = true;
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

        connecting = true;
        //データストアでの検索を行う
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null) Debug.Log($"検索に失敗しました。エラーコード{e.ErrorCode}");
            else
            {
                Debug.Log($"検索に成功しました。");
                topRanking = objList.ToArray();
            }
        });
    }

    public static void GetAroundMyScores(int currentScore, int stageNum)
    {
        // スキップする数を決める（ただし自分が1位か2位のときは調整する）
        int numSkip = currentRank - 2;
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
        query.Limit = 3;
        connecting = true;
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

    public static string[] GetNames(NCMBObject[] objList)
    {
        List<string> names = new List<string>();
        foreach (var o in objList) names.Add(System.Convert.ToString(o["name"]));
        return names.ToArray();
    }

    public static int[] GetScores(NCMBObject[] objList)
    {
        List<int> scores = new List<int>();
        foreach (var o in objList) scores.Add(System.Convert.ToInt32(o["score"]));
        return scores.ToArray();
    }
}
