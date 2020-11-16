using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningBlast_Y : MonoBehaviour
{
    //このScriptはプレイヤーにつけます

    private float pullTime = 0f;
    public GameObject morBlaSphere;    //おはようブラストの干渉判定用の球体
    private float plusScale = 0f;   //おはようブラストの放射範囲
    private GameObject morningBlast;
    private bool chargeFlg = true;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("B") && chargeFlg == true)
        {
            ChargeBlast();
        }
        else if ((Input.GetKeyUp("B") || pullTime >= 120f) && chargeFlg == true)   //2秒間押し続けるか、Bボタンを離したときに発動
        {
            ReleaseBlast();
            Invoke("DestroyBlast", 0.8f);
        }

        if(chargeFlg == false)
        {
            morningBlast.transform.localScale += new Vector3(plusScale, plusScale, plusScale);
        }
    }

    void ChargeBlast()
    {
        pullTime += 1f * Time.deltaTime;
    }

    void ReleaseBlast()
    {
        plusScale = pullTime / 120f;
        chargeFlg = false;
        morningBlast = Instantiate(morBlaSphere, this.transform);
        chargeFlg = false;
    }
    
    void DestroyBlast()
    {
        Destroy(morningBlast);
        chargeFlg = true;
    }
}
