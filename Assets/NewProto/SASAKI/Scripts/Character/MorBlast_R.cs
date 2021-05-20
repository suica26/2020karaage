using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorBlast_R : MonoBehaviour
{
    [SerializeField] private float maxPullTime;
    [SerializeField] private AudioClip chargeClip;
    [SerializeField] private AudioClip blastClip;
    [SerializeField] private AudioClip evoBlastClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transition_R scrAnim;
    [SerializeField] private float secondBlastTime, thirdBlastTime;
    [SerializeField] private float[] spreadScale;
    [SerializeField] private float[] spreadEvoScale;

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
    // Start is called before the first frame update
    void Start()
    {
        scrEvo = GetComponent<EvolutionChicken_R>();
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
            if(effect == null)
            {
                effect = Instantiate(_effect, center[scrEvo.EvolutionNum].position, Quaternion.Euler(transform.rotation.eulerAngles), transform);
                effect.transform.localScale *= effectScale[scrEvo.EvolutionNum];
                PlayEffect();
            }

            if(charge < 3)
            {
                //チャージ音を鳴らす
                if (!(audioSource.isPlaying == chargeClip))
                    audioSource.PlayOneShot(chargeClip);
            }

            //チャージ段階の判定
            pullTime += Time.deltaTime;
            if (pullTime >= 1f && charge == 0)
                charge = 1;
            if (pullTime >= 2f && charge == 1)
                charge = 2;
            if (pullTime >= 3f && charge == 2)
                charge = 3;
        }
        if (Input.GetMouseButtonUp(2))   //マウス中ボタンを離した際に発動
        {
            if (effect != null)
                Destroy(effect);

            audioSource.Stop();
            pullTime = 0f;
            if(charge > 0)
            {
                isBlast = true;
                StartCoroutine("ReleaseBlast");
            }
        }

        
        for(int i = 0; i < morningBlast.Length; i++)
        {
            if (morningBlast[i] != null)
            {
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
        scrAnim.SetAnimator(Transition_R.Anim.BLAST, true);

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
        scrAnim.SetAnimator(Transition_R.Anim.BLAST, false);
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
