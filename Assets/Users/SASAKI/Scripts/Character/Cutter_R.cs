using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter_R : MonoBehaviour
{
    [SerializeField] private GameObject preCutter;
    [SerializeField] private GameObject setGround;

    [SerializeField] private bool MoveToCamRot;     // カメラの方向にカッターを射出するか否か

    [SerializeField] private Transform[] cutterTransform;
    [SerializeField] private Transform[] backAreaTransform;
    [SerializeField] private float[] cutterSize;
    [Tooltip("カッターを投げる際の初速"), SerializeField] private float cutterBaseSpeed;
    [Tooltip("落下攻撃時のカッターの初速"), SerializeField] private float cutterFABaseSpeed;
    [Tooltip("落下攻撃時のカッターが地面に到達するまでの速度"), SerializeField] private float dropSpeed;

    [SerializeField] Transition_R[] scrAnim;

    private float timer;
    private float catchableTimer;
    private float animTimer;
    public bool throwingCutter = false;

    public bool CanCutter { get; set; }

    GameObject cutter;
    CharaMoveRigid_R scrMove;
    EvolutionChicken_R scrEvo;

    private CriAtomSource Sound;

    // Mobile Setting
    private bool mobileMode;
    private enum eCutter
    {
        wait,
        push,
        release,
    }

    private eCutter isCutter;

    // Start is called before the first frame update
    void Start()
    {
        scrMove = GetComponent<CharaMoveRigid_R>();
        scrEvo = GetComponent<EvolutionChicken_R>();
        timer = 0.0f;
        catchableTimer = 0.0f;
        animTimer = 0f;
        throwingCutter = false;
        CanCutter = true;
        Sound = GetComponent<CriAtomSource>();

        mobileMode = MobileSetting_R.GetInstance().IsMobileMode();
        isCutter = eCutter.wait;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        
        //カッターを投げている際にタイマー(カッター取得用)を加算    
        if (throwingCutter)
        {
            catchableTimer += Time.deltaTime;

            if (catchableTimer >= 5.0f)
            {
                catchableTimer = 0.0f;
                throwingCutter = false;
            }
        }

        if(AttackRestrictions_R.GetInstance().CanAttack())
        {
            //マウスクリック中にタイマー(通常or落下攻撃判定用)を加算
            if (!mobileMode)
            {
                if (Input.GetMouseButton(1) && CanCutter)
                    timer += Time.deltaTime;
            }
            else
            {
                if (isCutter == eCutter.push && CanCutter)
                    timer += Time.deltaTime;
            }

            //以下通常のカッター攻撃の記述
            if (!mobileMode)
            {
                if (Input.GetMouseButtonUp(1) && CanCutter)
                {
                    Cutter();
                }
            }
            else
            {
                if (isCutter == eCutter.release && CanCutter)
                    Cutter();
            }
        }

        //カッター攻撃のアニメーション遷移処理
        if (animTimer != 0f)
        {
            animTimer -= Time.deltaTime;
            if (animTimer <= 0f)
            {
                animTimer = 0f;
                scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.CUTTER, false);
            }
        }
    }

    public void Cutter()
    {
        if (!throwingCutter && timer <= 0.5f)
        {
            if (scrMove._isFlying)
            {
                scrMove._isFlying = false;
                GetComponent<Rigidbody>().useGravity = true;
            }

            AttackRestrictions_R.GetInstance().SetTimer(0.75f);

            throwingCutter = true;
            timer = 0.0f;
            animTimer = 0.25f;

            if (MoveToCamRot)
                cutter = Instantiate(preCutter, cutterTransform[scrEvo.EvolutionNum].position, Quaternion.Euler(Camera.main.transform.rotation.eulerAngles));
            else
                cutter = Instantiate(preCutter, cutterTransform[scrEvo.EvolutionNum].position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 0)));

            cutter.transform.localScale = cutter.transform.localScale * cutterSize[scrEvo.EvolutionNum];
            cutter.GetComponent<CutterMoveFA_R>().enabled = false;
            CutterMove1_R scrCutter = cutter.GetComponent<CutterMove1_R>();
            scrCutter.enabled = true;
            scrCutter.evoSpeed = 1.0f + scrEvo.EvolutionNum * 4.0f;
            scrCutter.backArea = backAreaTransform[scrEvo.EvolutionNum];
            scrCutter.cutterBaseSpeed = cutterBaseSpeed;
            scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.CUTTER, true);
            Sound.Play("CutterMove");

        }
        else
        {
            timer = 0.0f;
        }

        if (mobileMode)
            isCutter = eCutter.wait;
    }

    //以下は落下攻撃の際の処理を記述
    public void CutterAttack()
    {
        timer = 0.0f;
        throwingCutter = true;
        cutter = Instantiate(preCutter, cutterTransform[scrEvo.EvolutionNum].position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 0)));
        cutter.transform.localScale = cutter.transform.localScale * cutterSize[scrEvo.EvolutionNum];
        cutter.GetComponent<CutterMove1_R>().enabled = false;
        CutterMoveFA_R scrCutter = cutter.GetComponent<CutterMoveFA_R>();
        scrCutter.enabled = true;
        scrCutter.evoSpeed = 1.0f + scrEvo.EvolutionNum * 4.0f;
        scrCutter.backArea = backAreaTransform[scrEvo.EvolutionNum];
        scrCutter.cutterBaseSpeed = cutterFABaseSpeed;
        scrCutter.dropSpeed = dropSpeed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Cutter(Clone)" && catchableTimer >= 1.0f)
        {
            catchableTimer = 0.0f;
            Destroy(other.gameObject);
            throwingCutter = false;
        }
    }

    // Mobile Setting
    public void PushButton()
    {
        isCutter = eCutter.push;
    }

    public void ReleaseButton()
    {
        isCutter = eCutter.release;
    }
}
