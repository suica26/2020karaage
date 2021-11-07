using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionChicken_R : MonoBehaviour
{
    [SerializeField] private GameObject objParam;
    [SerializeField] private GameObject[] chickens;

    [SerializeField] private int[] evolutionPoint;

    [SerializeField] private float[] HP;
    [SerializeField] private float[] ATK;
    [SerializeField] private float[] SPD;
    [SerializeField] private float[] SCORE;
    [SerializeField] private float[] JUMP;

    [SerializeField] private GameObject EvoEffect;

    [SerializeField] private bool DEBUG_MODE;

    private Parameters_R scrParam;
    private MorBlast_R scrBlast;
    private int EP;

    private TpsCameraJC_R scrCam;
    private CriAtomSource Sound;

    //ステータス設定用変数
    private int evolutionNum;
    private float status_HP;
    private float status_ATK;
    private float status_SPD;
    private float status_SCORE;
    private float status_JUMP;

    //M よそから参照したいのでpublicにします
    public int nowEvoNum = 0;

    //カプセル化
    public int EvolutionNum { get { return evolutionNum; } }
    public float Status_HP { get { return status_HP; } }
    public float Status_ATK { get { return status_ATK; } }
    public float Status_SPD { get { return status_SPD; } }
    public float Status_SCORE { get { return status_SCORE; } }
    public float Status_JUMP { get { return status_JUMP; } }
    public int startNum;
    [SerializeField] private MonoBehaviour[] skills;

    void Start()
    {
        //if (ScoreAttack_Y.gameMode == mode.ScoreAttack) ScoreAttackSetting();

        // objParamが空の場合、Canvasオブジェクトを探す
        if (objParam == null)
            scrParam = objParam.gameObject.GetComponent<Parameters_R>();
        else
        {
            GameObject findCanvas = GameObject.Find("Canvas");
            if (findCanvas == null)
                Debug.LogError("Error: Parameters_R が存在していません! 詳しくは Evolution_Chiken_R を確認してください");
            else
                scrParam = findCanvas.GetComponent<Parameters_R>();
        }
        scrParam = objParam.gameObject.GetComponent<Parameters_R>();

        scrCam = Camera.main.GetComponent<TpsCameraJC_R>();

        scrBlast = GetComponent<MorBlast_R>();

        Sound = GetComponent<CriAtomSource>();

        //もともと0、startNumの数字=形態数
        evolutionNum = startNum;
        nowEvoNum = startNum;

        status_HP = HP[evolutionNum];
        status_ATK = ATK[evolutionNum];
        status_SPD = SPD[evolutionNum];
        status_SCORE = SCORE[evolutionNum];
        status_JUMP = JUMP[evolutionNum];

        //chickens[1].SetActive(false);
        //chickens[2].SetActive(false);
        //chickens[3].SetActive(false);

        //M　ループで最初の形態を決定
        for (int i = 0; i < 4; i++)
        {
            if (i == startNum)
            {
                chickens[i].SetActive(true);
            }
            else
            {
                chickens[i].SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (DEBUG_MODE)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                scrParam.EPManager(50);
            }
        }
        EP = scrParam.ep;

        if (evolutionNum < evolutionPoint.Length && EP >= evolutionPoint[evolutionNum])
        {
            chickens[evolutionNum].GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
            chickens[evolutionNum].GetComponent<Transition_R>().SetAnimator(Transition_R.Anim.EVOLUTION, true);

            //山本加筆　スコアアタック用
            if (ScoreAttack_Y.gameMode == mode.ScoreAttack) ScoreAttack_Y.AddLimitTime();

            evolutionNum++;

            Sound.Play("ShockWave");
            Camera.main.GetComponent<TpsCameraJC_R>().StartCoroutine("CameraWorkEvolution");

            var effect = Instantiate(EvoEffect, transform);
            effect.transform.localScale *= evolutionNum;
            Destroy(effect, 0.75f);
        }

        // 進化用
        if (scrCam.evolved && evolutionNum == nowEvoNum + 1)
        {
            nowEvoNum++;
            chickens[evolutionNum - 1].SetActive(false);
            chickens[evolutionNum].SetActive(true);

            chickens[evolutionNum - 1].GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;

            status_HP = HP[evolutionNum];
            status_ATK = ATK[evolutionNum];
            status_SPD = SPD[evolutionNum];
            status_SCORE = SCORE[evolutionNum];
            status_JUMP = JUMP[evolutionNum];

            scrBlast.EvoBlast();

            scrCam.evolved = false;
        }
    }

    private void ScoreAttackSetting()
    {
        EP = 0;
        startNum = 0;
        nowEvoNum = 0;
        evolutionNum = 0;
        evolutionPoint[0] = 450;
        evolutionPoint[1] = 1500;
        evolutionPoint[2] = 5500;
        foreach (var s in skills) s.enabled = true;
        ScoreAttack_Y.evoScr = this;
    }

    //ニワトリ退化
    public void Degenerate()
    {
        EP -= evolutionPoint[evolutionNum];
        chickens[evolutionNum].SetActive(false);
        evolutionNum--;
        nowEvoNum--;
        chickens[nowEvoNum].SetActive(true);
    }
}
