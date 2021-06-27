using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADX_Ray_Rev : MonoBehaviour
{
    //Rayを飛ばす距離
    public float distance = 8.0f;
    private float  RevSendLev;
    private Text RevSendLevel;
    public GameObject Debug_RevSendLevel, HitPoint,SoundDebudPanel;
    private bool A;
    private float a,b,c,d,e,f,g,h,i,j;
    private bool DebugMode;

    public float span = 0.2f;
    private float currentTime = 0f;
    private float True_RevSendLev;

    //プロパティー
    public float ADX_BusSendLevel
    {
        get { return this.RevSendLev; }  //取得用
        private set { this.RevSendLev = value; } //値入力用
    }

    // Start is called before the first frame update
    void Start()
    {
        this.RevSendLevel = Debug_RevSendLevel.GetComponent<Text>();
        DebugMode  = false;
        //SoundDebudPanel.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //span秒に1回だけ処理する
        currentTime += Time.deltaTime;
        if (currentTime > span)
        {

         a = Rays(0, -1, 0); //下
         b = Rays(0, 1, 0);//上
         c = Rays(-1, 0, 0);//左
         d = Rays(1, 0, 0);//右
         e = Rays(0, 0, 1);//前
         f = Rays(0, 0, -1);//後
         g = Rays(1, 0, -1);//右後
         h = Rays(1, 0, 1);//右前
         i = Rays(-1, 0, -1);//左後
         j = Rays(-1, 0, 1);//左前

         RevSendLev = (a + b + c + d + e + f + g + h + i + j - 1f) / 10f;//追々修正する
            True_RevSendLev = RevSendLev + 0.1f;
            this.RevSendLevel.text = "RevSendLevel:" + True_RevSendLev;
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
    private float Rays(float X,float Y,float Z)
    {
        //Rayの作成　　　　　　　Rayを飛ばす原点　　                                  　,飛ばす方向
        Ray ray = new Ray(transform.position + new Vector3(0.0f, 1.0f, 0.0f), new Vector3(X, Y, Z));

        //Rayが当たったオブジェクトの情報を収納
        RaycastHit hit;


        //Rayの可視化 
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);

        //もしRayにオブジェクトが衝突したら
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.tag == "Building")
            {
              if(DebugMode == true)//デバッグモード
              {
                //可視化点生成
                GameObject Spere = Instantiate(HitPoint, new Vector3(0, -10, 0), Quaternion.identity);
                //可視化点移動
            　　 Spere.transform.position = hit.point;
                    //ｎフレーム後に実行する//可視化点削除
                    StartCoroutine(DelayMethod(8, () =>{Destroy(Spere);}));
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
