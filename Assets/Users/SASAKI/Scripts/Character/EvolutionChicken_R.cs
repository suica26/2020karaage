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

    private CriAtomSource audioLavel;
    private CriAtomSource BGM;

    //M
    public int startNum;

    void Start()
    {
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

        audioLavel = (CriAtomSource)GetComponent("CriAtomSource");
        //ADX Selector Change
        audioLavel.player.SetSelectorLabel("Chicken_Form", "form1");

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
            evolutionNum++;

            Camera.main.GetComponent<TpsCameraJC_R>().StartCoroutine("CameraWorkEvolution");

            var effect = Instantiate(EvoEffect, transform);
            effect.transform.localScale *= evolutionNum;
            Destroy(effect, 0.75f);
        }

        // 進化用
        if(scrCam.evolved && evolutionNum == nowEvoNum + 1)
        {
            nowEvoNum++;
            chickens[evolutionNum - 1].SetActive(false);
            chickens[evolutionNum].SetActive(true);

            SoundFormCheck();
            audioLavel.Play("ShockWave");

            status_HP = HP[evolutionNum];
            status_ATK = ATK[evolutionNum];
            status_SPD = SPD[evolutionNum];
            status_SCORE = SCORE[evolutionNum];
            status_JUMP = JUMP[evolutionNum];

            scrBlast.EvoBlast();

            scrCam.evolved = false;
        }
    }

    private void SoundFormCheck()
    {
        if (evolutionNum == 1)
        {
            //ADX Selector Change
            audioLavel.player.SetSelectorLabel("Chicken_Form", "form2");
        }
        if (evolutionNum == 2)
        {
            audioLavel.player.SetSelectorLabel("Chicken_Form", "form3");
        }
        if (evolutionNum == 4)
        {
            audioLavel.player.SetSelectorLabel("Chicken_Form", "form4");
        }
    }
}
