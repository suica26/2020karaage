using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorBlast_R : MonoBehaviour
{
    [SerializeField] private float maxPullTime;
    [SerializeField] private AudioClip blastClip;
    [SerializeField] private float secondBlastTime, thirdBlastTime;
    [SerializeField] private float[] spreadScale;
    public GameObject morBlaSphere;    //おはようブラストの干渉判定用の球体
    private float plusScale = 0f;   //おはようブラストの放射範囲
    private GameObject[] morningBlast = new GameObject[3];
    private bool chargeFlg = true;
    public float spreadTime = 0.5f;    //おはようブラストの放射時間
    private bool releaseFlg;
    public int Number = 0;

    private float pullTime = 0f;

    private EvolutionChicken_R scrEvo;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        scrEvo = GetComponent<EvolutionChicken_R>();
        audioSource = GetComponent<AudioSource>();
        releaseFlg = false;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        releaseFlg = Input.GetMouseButtonUp(2) && pullTime > 0.5f;

        if (Input.GetMouseButton(2) && chargeFlg == true)
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
        if(pullTime < maxPullTime)
        {
            pullTime += Time.deltaTime;
        }
        else if(pullTime >= maxPullTime)
        {
            releaseFlg = true;
        }
    }

    IEnumerator ReleaseBlast()
    {
        plusScale = pullTime / maxPullTime + spreadScale[scrEvo.EvolutionNum];
        pullTime = 0f;
        chargeFlg = false;
        releaseFlg = false;
        audioSource.PlayOneShot(blastClip);

        //1回目
        morningBlast[0] = Instantiate(morBlaSphere, transform);
        Destroy(morningBlast[0], spreadTime);
        yield return new WaitForSeconds(secondBlastTime);
        //2回目
        morningBlast[1] = Instantiate(morBlaSphere, transform);
        Destroy(morningBlast[1], spreadTime);
        yield return new WaitForSeconds(thirdBlastTime);
        //3回目
        morningBlast[2] = Instantiate(morBlaSphere, transform);
        Destroy(morningBlast[2], spreadTime);
        chargeFlg = true;
        yield break;
    }

    //進化時のブラスト生成
    public void EvoBlast()
    {
        pullTime = 1.0f;
        StartCoroutine("ReleaseBlast");
    }
}
