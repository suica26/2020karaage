using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADX_Ray_Rev : MonoBehaviour
{
    //Rayの飛ばせる距離
    public float distance = 5.0f;
    private float RevSendLev;
    private Text RevSendLevel;
    public GameObject Debug_RevSendLevel, HitPoint;
    private float RevLevel;
    private bool A;
    private int a,b,c,d,e,f,g,h,i,j;

    // Start is called before the first frame update
    void Start()
    {
        this.RevSendLevel = Debug_RevSendLevel.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
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

        RevSendLev = a + b + c + d + e + f + g + h + i + j;

        this.RevSendLevel.text = "RevSendLevel:"+ RevSendLev;

    }

    private int Rays(float X,float Y,float Z)
    {
        //当たり点生成
        GameObject Spere = Instantiate(HitPoint, new Vector3(0, -10, 0), Quaternion.identity);

        //Rayの作成　　　　　　　Rayを飛ばす原点　　                                  　,Rayを飛ばす方向
        Ray ray = new Ray(transform.position + new Vector3(0.0f, 1.0f, 0.0f), new Vector3(X, Y, Z));

        //Rayが当たったオブジェクトの情報を入れる
        RaycastHit hit;


        //Rayの可視化    ↓Rayの原点　　
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
            //もしRayにオブジェクトが衝突したら
            //                  Rayが当たったオブジェクト/距離
        if (Physics.Raycast(ray, out hit, distance))
        {
            Spere.transform.position = hit.point;

            if (hit.collider.tag == "Building")
            {
                A = true;
            }
            else
            {
                A = false;
            }
        }

        //当たり点削除
        Destroy(Spere);

        //Aがtrueなら返り値は　1
        //Aがfalseなら返り値は　0
        return A ? 1 : 0;
    }
}
