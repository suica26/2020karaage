using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADX_Surround_Ray : MonoBehaviour
{
    public GameObject HitPoint;
    private bool DebugMode;
    private float Num;
    public float span = 0.2f;
    private float currentTime = 0f;
    private float a, b, c, d, TownNoizeNum;

    private CriAtomSource Sound;

    // Start is called before the first frame update
    void Start()
    {
        Sound = GetComponent<CriAtomSource>();
        //NoizeEQControl AISAC
        Sound.SetAisacControl("Obj_angle", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //span秒に1回だけ処理する
        currentTime += Time.deltaTime;
        if (currentTime > span)
        {

            a = Rays(-1, 0.5f, -1, 8);//左後上
            b = Rays(1, 0.5f, 1, 8);//右前上
            c = Rays(1, 0.5f, -1, 8);//右後上
            d = Rays(-1, 0.5f, 1, 8);//左前上
            /*
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
            */


            currentTime = 0f;

            //木にレイがあったていれば
            if (a == -1 | b == -1 | c == -1 | d == -1 )
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

            //NoizeControl AISAC
            Sound.SetAisacControl("NoizeControl", TownNoizeNum);
        }
        //デバッグモードONOFF
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.L) & DebugMode == false)
        {
            DebugMode = true;
            //SoundDebudPanel.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.LeftShift) & Input.GetKeyDown(KeyCode.K) & DebugMode == true)
        {
            DebugMode = false;
            //SoundDebudPanel.gameObject.SetActive(false);
        }
    }

    //RayCastメソッド
    private float Rays(float X, float Y, float Z, float distance)
    {
        //Rayの作成　　　　　　　Rayを飛ばす原点　　                                  　,飛ばす方向
        Ray ray = new Ray(transform.position, transform.rotation * new Vector3(X, Y, Z));
        //Rayが当たったオブジェクトの情報を収納
        RaycastHit hit;
        //Rayの可視化 
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.blue);

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
