using System;
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
    private bool A;
    private float a, b, c, d, e, f, g, h, i, j,k ,l, m;
    private bool DebugMode;

    public float span = 0f;
    private float currentTime = 0f;
    private float True_RevSendLev;

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
        //SoundDebudPanel.gameObject.SetActive(false);

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
            i = Rays(0, -1, 0, 8);//下
            j = Rays(1, -1, 1, 8);//右前下
            k = Rays(1, -1, -1, 8);//右後下
            l = Rays(-1, -1, 1, 8);//左前下
            m = Rays(-1, -1, -1, 8);//左後下

            RevSendLev_L = (a + e + f + j + k) / 10;//Lch_SendLevel
            RevSendLev_R = (b + g + h + l + m) / 10;//Rch_SendLevel

            this.RevSendLevel.text = "RevSendLevel_L:" + RevSendLev_L + "RevSendLevel_R:" + RevSendLev_R;
            currentTime = 0f;
        }

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
            if (hit.collider.tag == "Building")
            {
                if (DebugMode == true)//デバッグモード
                {
                    //可視化点生成
                    GameObject Spere = Instantiate(HitPoint, new Vector3(0, -10, 0), Quaternion.identity);
                    //可視化点移動
                    Spere.transform.position = hit.point;
                    //ｎフレーム後に実行する//可視化点削除
                    StartCoroutine(DelayMethod(8, () => { Destroy(Spere); }));
                }
                A = true;
            }

        }
        else
        {
            A = false;
        }

        //Aがtrueなら返り値は　1
        //Aがfalseなら返り値は　0
        return A ? 1f : 0f;
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
