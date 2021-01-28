using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorBlast_R : MonoBehaviour
{
    //このScriptはプレイヤーにつけます

    [SerializeField] private float pullTime = 0f;
    [SerializeField] private AudioClip blastClip;
    [SerializeField] private float secondBlastTime, thirdBlastTime;
    public GameObject morBlaSphere;    //おはようブラストの干渉判定用の球体
    private float plusScale = 0f;   //おはようブラストの放射範囲
    private GameObject[] morningBlast = new GameObject[3];
    private bool chargeFlg = true;
    public float spreadTime = 0.5f;    //おはようブラストの放射時間
    private bool releaseFlg;
    public int Number = 0;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        releaseFlg = false;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        releaseFlg = Input.GetKeyUp(KeyCode.B) && pullTime > 0.5f;

        if (Input.GetKey(KeyCode.B) && chargeFlg == true)
        {
            ChargeBlast();
        }
        if (releaseFlg == true && chargeFlg == true)   //2秒間押し続けるか、Bボタンを離したときに発動
        {
            StartCoroutine("ReleaseBlast");
        }

        
        for(int i = 0; i < morningBlast.Length; i++)
        {
            if (morningBlast[i] != null)
            {
                morningBlast[i].transform.localScale += new Vector3(plusScale, plusScale, plusScale);
            }   
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Number++;
            if (Number == 3)
            {
                Number = 0;
            }
        }
    }

    void ChargeBlast()
    {
        if(pullTime < 2f)
        {
            pullTime += Time.deltaTime;
        }
        else if(pullTime >= 2f)
        {
            releaseFlg = true;
        }
    }

    IEnumerator ReleaseBlast()
    {
        plusScale = pullTime / 2f;
        pullTime = 0f;
        chargeFlg = false;
        releaseFlg = false;
        audioSource.PlayOneShot(blastClip);

        //1回目
        morningBlast[0] = Instantiate(morBlaSphere, this.transform);
        Destroy(morningBlast[0], spreadTime);
        yield return new WaitForSeconds(secondBlastTime);
        //2回目
        morningBlast[1] = Instantiate(morBlaSphere, this.transform);
        Destroy(morningBlast[1], spreadTime);
        yield return new WaitForSeconds(thirdBlastTime);
        //3回目
        morningBlast[2] = Instantiate(morBlaSphere, this.transform);
        Destroy(morningBlast[2], spreadTime);
        chargeFlg = true;
        yield break;
    }
}
