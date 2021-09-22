using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenKick_R : MonoBehaviour
{
    [SerializeField] GameObject[] kickCollisions;
    [SerializeField] GameObject kickEffect;
    [SerializeField] Transition_R[] scrAnim;

    EvolutionChicken_R scrEvo;
    CharaMoveRigid_R scrMove;

    public bool CanKick { get; set; }
    public int chargePoint;
    private float timer;

    //山本加筆　キックのクールタイム
    [SerializeField] private float[] coolTimes;
    private float coolTimer;

    //ADX
    private CriAtomSource criAtomSource;

    // Mobile Setting
    private bool mobileMode;
    private enum eKick
    {
        wait,
        push,
        release,
    }
    private eKick isKick;

    // Start is called before the first frame update
    void Start()
    {
        scrMove = GetComponent<CharaMoveRigid_R>();
        scrEvo = GetComponent<EvolutionChicken_R>();
        timer = 0.0f;
        CanKick = true;
        chargePoint = 0;
        criAtomSource = GetComponent<CriAtomSource>();

        mobileMode = MobileSetting_R.GetInstance().IsMobileMode();
        isKick = eKick.wait;
    }

    // Update is called once per frame
    void Update()
    {
        coolTimer += Time.deltaTime;

        //M 追加:時間停止中は操作を遮断
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (!mobileMode)
            if (Input.GetMouseButton(0) && CanKick)
                timer += Time.deltaTime;
        else
            if (isKick == eKick.push && CanKick)
                timer += Time.deltaTime;
        if(AttackRestrictions_R.GetInstance().CanAttack())
        {
            if (!mobileMode)
            {
                if (Input.GetMouseButtonUp(0) && CanKick)
                {
                    //山本加筆(&& coolTimer >= coolTimes[scrEvo.EvolutionNum])
                    if (timer <= 0.5f)
                    {
                        Kick();
                    }
                    else
                    {
                        timer = 0.0f;
                    }
                }
                else
                {
                    kickCollisions[scrEvo.EvolutionNum].SetActive(false);
                    scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.KICK, false);
                }
            }
            else
            {
                if (isKick == eKick.release && CanKick)
                {
                    if (timer <= 0.5f)
                        Kick();
                    else
                        timer = 0.0f;
                }
                else
                {
                    kickCollisions[scrEvo.EvolutionNum].SetActive(false);
                    scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.KICK, false);
                }
            }
        }
    }

    private void Kick()
    {
        if (coolTimer >= coolTimes[scrEvo.EvolutionNum])
        {
            if (scrMove._isFlying)
            {
                scrMove._isFlying = false;
                GetComponent<Rigidbody>().useGravity = true;
            }

            AttackRestrictions_R.GetInstance().SetTimer(0.25f);

            timer = 0.0f;
            coolTimer = 0f;
            //audioSource.PlayOneShot(kickSound);
            criAtomSource.Play("Kick");
            var objKick = Instantiate(kickEffect, transform.position, Quaternion.identity);
            Destroy(objKick, 0.5f);
            kickCollisions[scrEvo.EvolutionNum].SetActive(true);
            scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.KICK, true);
        }

        if (mobileMode)
            isKick = eKick.wait;
    }

    // Mobile Button
    public void PushButton()
    {
        isKick = eKick.push;
        Debug.LogError("PUSH");
    }

    public void ReleaseButton()
    {
        isKick = eKick.release;
        Debug.LogError("Release");
    }
}
