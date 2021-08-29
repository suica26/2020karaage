using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorBlast_R : MonoBehaviour
{
    [SerializeField] private AudioClip chargeClip;
    [SerializeField] private AudioClip blastClip;
    [SerializeField] private AudioClip evoBlastClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transition_R[] scrAnim;
    [SerializeField] private float secondBlastTime, thirdBlastTime;
    [SerializeField] private float[] pullTimes;     // チャージの時間を設定する
    [SerializeField] private float[] spreadScale;
    [SerializeField] private float[] spreadEvoScale;
    [SerializeField] private Transform[] blastCenter;  // ブラストの中心位置
    [SerializeField] private float moveSpeedMag;    // 減衰率(0 ~ 1で設定)

    [SerializeField] private GameObject _effect;
    [SerializeField] private Transform[] center;
    [SerializeField] private float[] effectScale;

    public GameObject morBlaSphere;    //おはようブラストの干渉判定用の球体
    private float plusScale = 0f;   //おはようブラストの放射範囲
    private GameObject[] morningBlast = new GameObject[3];
    private GameObject effect;
    private int charge;
    private bool isBlast;   //ブラスト発声中か否かのフラグ
    public float spreadTime;    //おはようブラストの放射時間
    public int Number = 0;

    private float pullTime = 0f;

    private EvolutionChicken_R scrEvo;

    // 使用制限等を設定
    private CharaMoveRigid_R scrMove;
    private chickenKick_R scrKick;
    private Cutter_R scrCutter;

    private new CriAtomSource audio;

    // Start is called before the first frame update
    void Start()
    {
        scrEvo = GetComponent<EvolutionChicken_R>();
        scrMove = GetComponent<CharaMoveRigid_R>();
        scrKick = GetComponent<chickenKick_R>();
        scrCutter = GetComponent<Cutter_R>();
        audio = GetComponent<CriAtomSource>();
        Debug.LogError("scrKick: " + scrKick);
        Debug.LogError("scrCutter: " + scrCutter);
        isBlast = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (Input.GetMouseButton(2) && !isBlast)
        {
            // 移動速度の減衰
            if (scrMove != null)
                scrMove.MoveMag = 1.0f - moveSpeedMag;

            // キックを使用不可にする
            if (scrKick != null)
                scrKick.CanKick = false;

            // カッターを使用不可にする
            if (scrCutter != null)
                scrCutter.CanCutter = false;

            if(effect == null)
            {
                effect = Instantiate(_effect, center[scrEvo.EvolutionNum].position, Quaternion.Euler(transform.rotation.eulerAngles), transform);
                effect.transform.localScale *= effectScale[scrEvo.EvolutionNum];
                PlayEffect();
            }


            //チャージ音を鳴らす
            audio.Play("BlastSub01");
           

            //チャージ段階の判定
            pullTime += Time.deltaTime;
            if (pullTime >= pullTimes[0] && charge == 0)
                charge = 1;
            if (pullTime >= pullTimes[1] && charge == 1)
                charge = 2;
            if (pullTime >= pullTimes[2] && charge == 2)
                charge = 3;
        }

        if (Input.GetMouseButtonUp(2))   //マウス中ボタンを離した際に発動
        {
            // 移動速度を直す
            if (scrMove != null)
                scrMove.MoveMag = 1.0f;

            // キックを使用可能にする
            if (scrKick != null)
                scrKick.CanKick = true;

            // カッターを使用可能にする
            if (scrCutter != null)
                scrCutter.CanCutter = true;

            if (effect != null)
                Destroy(effect);

            audioSource.Stop();
            pullTime = 0f;
            if(charge > 0)
            {
                isBlast = true;
                StartCoroutine("ReleaseBlast");
            }

            audio.Stop();
            audio.Play("BlastSub02");
        }

        
        for(int i = 0; i < morningBlast.Length; i++)
        {
            if (morningBlast[i] != null)
            {
                morningBlast[i].transform.position = blastCenter[scrEvo.EvolutionNum].position;
                morningBlast[i].transform.localScale += new Vector3(plusScale, plusScale, plusScale) / spreadTime * Time.deltaTime;
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

    IEnumerator ReleaseBlast()
    {
        plusScale = spreadScale[charge - 1] * spreadEvoScale[scrEvo.EvolutionNum];
        pullTime = 0f;
        charge = 0;
        audioSource.PlayOneShot(blastClip);
        scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.BLAST, true);

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

        isBlast = false;
        scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.BLAST, false);
        yield break;
    }

    //進化時のブラスト生成
    public void EvoBlast()
    {
        plusScale = spreadScale[2] * spreadEvoScale[scrEvo.EvolutionNum];
        audioSource.PlayOneShot(evoBlastClip);
        morningBlast[0] = Instantiate(morBlaSphere, transform);
        Destroy(morningBlast[0], spreadTime);
    }

    private void PlayEffect()
    {
        if(effect != null)
        {
            foreach(var particle in effect.GetComponentsInChildren<ParticleSystem>())
            {
                particle.Play();
            }
        }
    }
}
