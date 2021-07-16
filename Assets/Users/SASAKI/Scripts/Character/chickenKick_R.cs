using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenKick_R : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject[] kickCollisions;
    [SerializeField] GameObject kickEffect;
    [SerializeField] AudioClip kickSound;
    [SerializeField] Transition_R[] scrAnim;

    EvolutionChicken_R scrEvo;
    CharaMoveRigid_R scrMove;

    public int chargePoint;
    private float timer;

    //山本加筆　キックのクールタイム
    [SerializeField] private float[] coolTimes;
    private float coolTimer;

    //ADX
    private CriAtomSource criAtomSource;

    // Start is called before the first frame update
    void Start()
    {
        scrMove = GetComponent<CharaMoveRigid_R>();
        scrEvo = GetComponent<EvolutionChicken_R>();
        timer = 0.0f;
        chargePoint = 0;
        criAtomSource = GetComponent<CriAtomSource>();
    }

    // Update is called once per frame
    void Update()
    {
        coolTimer += Time.deltaTime;

        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //山本加筆(&& coolTimer >= coolTimes[scrEvo.EvolutionNum])
            if (timer <= 0.5f)
            {
                if (coolTimer >= coolTimes[scrEvo.EvolutionNum])
                {
                    if (scrMove._isFlying)
                    {
                        scrMove._isFlying = false;
                        GetComponent<Rigidbody>().useGravity = true;
                    }

                    timer = 0.0f;
                    coolTimer = 0f;
                    //audioSource.PlayOneShot(kickSound);
                    criAtomSource.Play("Kick");
                    var objKick = Instantiate(kickEffect, transform.position, Quaternion.identity);
                    Destroy(objKick, 0.5f);
                    kickCollisions[scrEvo.EvolutionNum].SetActive(true);
                    scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.KICK, true);
                }
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
}
