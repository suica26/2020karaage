using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter_R : MonoBehaviour
{
    [SerializeField] private GameObject preCutter;
    [SerializeField] private GameObject setGround;

    [SerializeField] private Transform[] cutterTransform;
    [SerializeField] private Transform[] backAreaTransform;
    [SerializeField] private float[] cutterSize;
    [Tooltip("カッターを投げる際の初速"), SerializeField] private float cutterBaseSpeed;
    [Tooltip("落下攻撃時のカッターの初速"), SerializeField] private float cutterFABaseSpeed;
    [Tooltip("落下攻撃時のカッターが地面に到達するまでの速度"), SerializeField] private float dropSpeed;

    [SerializeField] Transition_R scrAnim;

    private float timer;
    private float catchableTimer;
    private float animTimer;
    public bool throwingCutter = false;

    GameObject cutter;
    CharaMoveRigid_R scrMove;
    EvolutionChicken_R scrEvo;
    // Start is called before the first frame update
    void Start()
    {
        scrMove = GetComponent<CharaMoveRigid_R>();
        scrEvo = GetComponent<EvolutionChicken_R>();
        timer = 0.0f;
        catchableTimer = 0.0f;
        animTimer = 0f;
        throwingCutter = false;
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

        //マウスクリック中にタイマー(通常or落下攻撃判定用)を加算
        if (Input.GetMouseButton(1))
        {
            timer += Time.deltaTime;
        }

        //以下通常のカッター攻撃の記述
        if (Input.GetMouseButtonUp(1))
        {
            if(!throwingCutter && timer <= 0.5f)
            {
                if (scrMove._isFlying)
                {
                    scrMove._isFlying = false;
                    GetComponent<Rigidbody>().useGravity = true;
                }

                throwingCutter = true;
                timer = 0.0f;
                animTimer = 0.25f;
                cutter = Instantiate(preCutter, cutterTransform[scrEvo.EvolutionNum].position, Quaternion.Euler(transform.rotation.eulerAngles));
                cutter.transform.localScale = cutter.transform.localScale * cutterSize[scrEvo.EvolutionNum];
                cutter.GetComponent<CutterMoveFA_R>().enabled = false;
                CutterMove1_R scrCutter = cutter.GetComponent<CutterMove1_R>();
                scrCutter.enabled = true;
                scrCutter.evoSpeed = cutterSize[scrEvo.EvolutionNum];
                scrCutter.backArea = backAreaTransform[scrEvo.EvolutionNum];
                scrCutter.cutterBaseSpeed = cutterBaseSpeed;
                scrAnim.SetAnimator(Transition_R.Anim.CUTTER, true);
            }
            else
            {
                timer = 0.0f;
            }
        }

        //カッター攻撃のアニメーション遷移処理
        if (animTimer != 0f)
        {
            animTimer -= Time.deltaTime;
            if (animTimer <= 0f)
            {
                animTimer = 0f;
                scrAnim.SetAnimator(Transition_R.Anim.CUTTER, false);
            }
        }
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
        scrCutter.evoSpeed = cutterSize[scrEvo.EvolutionNum];
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
}
