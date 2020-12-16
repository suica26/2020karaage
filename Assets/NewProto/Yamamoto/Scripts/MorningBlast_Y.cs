using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningBlast_Y : MonoBehaviour
{
    //このScriptはプレイヤーにつけます

    [SerializeField] private float pullTime = 0f;
    public GameObject morBlaSphere;    //おはようブラストの干渉判定用の球体
    private float plusScale = 0f;   //おはようブラストの放射範囲
    private GameObject morningBlast;
    private bool chargeFlg = true;
    public float spreadTime = 0.5f;    //おはようブラストの放射時間
    private bool releaseFlg;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        releaseFlg = Input.GetKeyUp(KeyCode.B) && pullTime > 30f;

        if (Input.GetKey(KeyCode.B) && chargeFlg == true)
        {
            ChargeBlast();
        }
        if (releaseFlg == true && chargeFlg == true)   //2秒間押し続けるか、Bボタンを離したときに発動
        {
            ReleaseBlast();
            Invoke("DestroyBlast", spreadTime);   //0.8秒間放射する
        }

        if(chargeFlg == false)
        {
            morningBlast.transform.localScale += new Vector3(plusScale, plusScale, plusScale);
        }
    }

    void ChargeBlast()
    {
        pullTime += 1f;
        if (pullTime >= 120f)
        {
            pullTime = 120f;
        }
    }

    void ReleaseBlast()
    {
        plusScale = pullTime / 120f;
        chargeFlg = false;
        morningBlast = Instantiate(morBlaSphere, this.transform);
        pullTime = 0f;
    }
    
    void DestroyBlast()
    {
        Destroy(morningBlast);
        chargeFlg = true;
    }
}
