﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADX_Ray_Rev : MonoBehaviour
{
    //Rayを飛ばす距離
    private float RevSendLev_R, RevSendLev_L;
    private Text RevSendLevel;
    public GameObject Debug_RevSendLevel, HitPoint, SoundDebudPanel;
    private bool A, Once, OnGround;
    private float a, b, c, d, e, f, g, h, i, j, k, l, m, o, p, q, r, TownNoizeNum, Num, TownNoizeEQNum, EQNum;
    private float Ypos;
    private bool DebugMode;

    public float span = 0f;
    private float currentTime = 0f;
    private float True_RevSendLev;

    private CriAtomSource Sound;
    public CriAtomSource TownNoizeCon;
    private string GroundMaterial;

    //プロパティー
    public float ADX_BusSendLevel_L
    {
        get { return this.RevSendLev_R; }  //取得用
        private set { this.RevSendLev_R = value; } //値入力用
    }
    public float ADX_BusSendLevel_R
    {
        get { return this.RevSendLev_L; }  //取得用
        private set { this.RevSendLev_L = value; } //値入力用
    }

    // Start is called before the first frame update
    void Start()
    {
        this.RevSendLevel = Debug_RevSendLevel.GetComponent<Text>();
        DebugMode = false;
        Sound = GetComponent<CriAtomSource>();
        Once = true;
        //NoizeControl AISAC
        TownNoizeNum = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //span秒に1回だけ処理する
        currentTime += Time.deltaTime;
        if (currentTime > span)
        {

            a = Rays(1, 0, 0, 8); //右
            b = Rays(-1, 0, 0, 8);//左
            c = Rays(0, 0, 1, 8);//前
            d = Rays(0, 0, -1, 8);//後
            e = Rays(1, 0, 1, 8);//右前
            f = Rays(1, 0, -1, 8);//右後
            g = Rays(-1, 0, 1, 8);//左前
            h = Rays(-1, 0, -1, 8);//左後
            i = Rays(0, -1, 0, 0.1f);//下
            j = Rays(1, -1, 1, 8);//右前下
            k = Rays(1, -1, -1, 8);//右後下
            l = Rays(-1, -1, 1, 8);//左前下
            m = Rays(-1, -1, -1, 8);//左後下
            o = Rays(-1, 1, -1, 8);//左後上
            p = Rays(1, 1, 1, 8);//右前上
            q = Rays(1, 1, -1, 8);//右後上
            r = Rays(-1, 1, 1, 8);//左前上

            RevSendLev_L = (a + e + f + j + k) / 10;//Lch_SendLevel
            RevSendLev_R = (b + g + h + l + m) / 10;//Rch_SendLevel


            currentTime = 0f;

            //TownNoizeNum = - (a + b + c + d + e + f + g + h + o + p + q + r)/8;

            //木にレイがあったていれば
            if (a == -1 | b == -1 | c == -1 | d == -1 | e == -1 | f == -1 | g == -1 | h == -1 | j == -1 | k == -1 | l == -1 | m == -1 | o == -1 | p == -1 | q == -1 | r == -1)
            {
                if (TownNoizeNum <= 1.0f)
                {
                    TownNoizeNum += 0.1f;
                }
            }
            else
            {
                if (TownNoizeNum >= 0.0f)
                {
                    TownNoizeNum -= 0.1f;
                }
            }

            TownNoizeEQNum = (a + b + c + d + e + f + g + h + j + k + l + m + o + p + q + r) / 20;

            if (TownNoizeEQNum >= 0.55f)
            {
                if (EQNum <= 1.0f)
                {
                    EQNum += 0.15f;
                }
            }
            else
            {
                if (EQNum >= 0.0f)
                {
                    EQNum -= 0.15f;
                }
            }



            //NoizeControl AISAC
            TownNoizeCon.SetAisacControl("NoizeControl", TownNoizeNum);
            //NoizeEQControl AISAC
            TownNoizeCon.SetAisacControl("NoizeEQControl", EQNum);


        }
        //高さ座標からAISAC値を決定
        Vector3 Pos = this.transform.position;
        Ypos = Pos.y / 24.0f;
        //Obj_angle AISAC
        TownNoizeCon.SetAisacControl("Obj_angle", Ypos);

        this.RevSendLevel.text = "RevSendLevel_L:" + RevSendLev_L + "RevSendLevel_R:" + RevSendLev_R + "\n" + "TownNoise:" + TownNoizeNum + "EQ:" + EQNum + "\n" + "Pos:" + Ypos;
        //接地＆材質判定
        OnGround = GloundRays(0.5f);

        if (OnGround == true & Once == false)
        {
            Sound.Play("Landing");
            Once = true;
        }
        Sound.player.SetSelectorLabel("Selector_Floor", GroundMaterial);

        //デバッグモードONOFF
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.L) & DebugMode == false)
        {
            DebugMode = true;
            SoundDebudPanel.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.LeftShift) & Input.GetKeyDown(KeyCode.K) & DebugMode == true)
        {
            DebugMode = false;
            SoundDebudPanel.gameObject.SetActive(false);
        }
    }

    //RayCastメソッド
    private float Rays(float X, float Y, float Z, float distance)
    {
        //Rayの作成　　　　　　　Rayを飛ばす原点　　                                  　,飛ばす方向
        Ray ray = new Ray(transform.position + new Vector3(0.0f, 1.0f, 0.0f), transform.rotation * new Vector3(X, Y, Z));
        //Rayが当たったオブジェクトの情報を収納
        RaycastHit hit;
        //Rayの可視化 
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);

        //もしRayにオブジェクトが衝突したら
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.tag == "Big")
            {
                Num = 1f;
            }

            if (hit.collider.tag == "Tree")
            {
                Num = -1f;
            }

            if (DebugMode == true)//デバッグモード
            {
                //可視化点生成
                GameObject Spere = Instantiate(HitPoint, new Vector3(0, -10, 0), Quaternion.identity);
                //可視化点移動
                Spere.transform.position = hit.point;
                //ｎフレーム後に実行する//可視化点削除
                StartCoroutine(DelayMethod(8, () => { Destroy(Spere); }));
            }
        }
        else
        {
            Num = 0f;
        }
        //Aがtrueなら返り値は　1
        //Aがfalseなら返り値は　0
        return Num;
    }

    //接地RayCastメソッド
    private bool GloundRays(float Glounddistance)
    {
        //Rayの作成　　　　
        Ray Gray = new Ray(transform.position, Vector3.down);
        RaycastHit Ghit;
        //Rayの可視化 
        Debug.DrawRay(Gray.origin, Gray.direction * Glounddistance, Color.black);
        //Rayにオブジェクトが衝突したら
        if (Physics.Raycast(Gray, out Ghit, Glounddistance))
        {
            OnGround = true;

            if (Ghit.collider.tag == "Soil")
            {
                GroundMaterial = "soil";
            }
            else
            {
                GroundMaterial = "asphalt";
            }
        }
        else
        {
            OnGround = false;
            Once = false;
        }
        return OnGround;
    }

    //コルーチン
    private IEnumerator DelayMethod(int delayFrameCount, Action action)
    {
        for (var i = 0; i < delayFrameCount; i++)
        {
            yield return null;
        }
        action();
    }
}