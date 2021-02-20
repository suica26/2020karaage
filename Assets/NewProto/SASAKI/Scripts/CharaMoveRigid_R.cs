﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMoveRigid_R : MonoBehaviour
{
    [SerializeField] private float[] raycastCubeX;
    [SerializeField] private float[] raycastCubeZ;
    [SerializeField] private float rotateSpeed;

    [Header("落下攻撃設定")]
    [SerializeField] private float[] circleRange;
    [SerializeField] private float circleKickRange;
    [SerializeField] private float addFanWidth;
    [SerializeField] private float addFanHeight;
    [SerializeField] private AudioClip CutterFAClip;
    [SerializeField] private AudioClip KickFAClip;
    [SerializeField] private AudioClip JumpClip;
    [SerializeField] private Transition_R scrAnim;
    [SerializeField] GameObject preCircle;
    [SerializeField] GameObject preFan;

    private Rigidbody rb;
    private float h, v;
    private Vector3 cameraForward = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded = true;
    private Ray ray;

    private AudioSource audioSource;

    private EvolutionChicken_R scrEvo;
    private Cutter_R scrCutter;
    private float speed;
    private float jumpSpeed;

    //落下攻撃用変数
    private bool fallAttack = false;
    private int fallAttackVer = 0;

    private float kickFallAttackTimer = 0f;
    private float kickFallAttackTime = 0.5f;
    private float cutterFallAttackTimer = 0f;
    private float cutterFallAttackTime = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        scrEvo = GetComponent<EvolutionChicken_R>();
        scrCutter = GetComponent<Cutter_R>();
    }

    void Update()
    {
        speed = scrEvo.Status_SPD;
        jumpSpeed = scrEvo.Status_JUMP;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        /*
        //足元から下へ向けて球状のRayを発射し，着地判定をする
        ray = new Ray(gameObject.transform.position + 0.15f * gameObject.transform.up, - gameObject.transform.up);
        isGrounded = Physics.SphereCast(ray, 0.13f, 0.08f);
        //着地判定の範囲をシーンに示す
        Debug.DrawRay(gameObject.transform.position + 0.2f * gameObject.transform.up, -0.22f * gameObject.transform.up);
        */

        //BoxCastで設置判定
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + transform.up * 0.1f, new Vector3(raycastCubeX[scrEvo.EvolutionNum] * 0.5f, 0.05f, raycastCubeZ[scrEvo.EvolutionNum] * 0.5f), -transform.up,
                           Quaternion.Euler(transform.rotation.eulerAngles), 0.1f);
        isGrounded = false;

        foreach(RaycastHit hit in  hits)
        {
            Debug.Log(hit.transform.gameObject.name);
            if(hit.transform.gameObject.tag != "Player")
            {
                isGrounded = true;
            }
        }

        if (isGrounded)
        {
            scrAnim.SetAnimator(Transition_R.Anim.JUMP, false);

            if (fallAttack)
            {
                scrAnim.SetAnimator(Transition_R.Anim.KICKFA, false);
                scrAnim.SetAnimator(Transition_R.Anim.CUTTERFA, false);
                fallAttack = false;
                fallAttackCollisionCheck();
                rb.velocity = Vector3.zero;
            }

            if (h != 0 || v != 0)
            {
                scrAnim.SetAnimator(Transition_R.Anim.WALK, true);
                cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                moveDirection = cameraForward * v + Camera.main.transform.right * h;
                if (moveDirection.magnitude > 1)
                {
                    moveDirection.Normalize();
                }

                rb.velocity = moveDirection * speed;

                if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                {
                    Quaternion qua = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, rotateSpeed * Time.deltaTime);
                }
            }
            else
            {
                scrAnim.SetAnimator(Transition_R.Anim.WALK, false);
            }

            if (Input.GetButtonDown("Jump"))
            {
                audioSource.PlayOneShot(JumpClip);
                scrAnim.SetAnimator(Transition_R.Anim.JUMP, true);
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }
        }
        else
        {
            scrAnim.SetAnimator(Transition_R.Anim.JUMP, true);
            scrAnim.SetAnimator(Transition_R.Anim.WALK, false);

            //空中での制動(移動量は地上の1/3程度)
            if (h != 0 || v != 0)
            {
                cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                moveDirection = cameraForward * v + Camera.main.transform.right * h;
                rb.AddForce(moveDirection * speed * 0.33f, ForceMode.Force);

                var velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                if(velocity.magnitude > speed)
                {
                    rb.velocity = velocity.normalized * speed + rb.velocity.y * Vector3.up;
                }
            }

            if (Input.GetMouseButton(1) && !fallAttack)
            {
                if(cutterFallAttackTimer <= cutterFallAttackTime)
                {
                    cutterFallAttackTimer += Time.deltaTime;
                }
                else if(!scrCutter.throwingCutter)
                {
                    fallAttackVer = 2;
                    StartCoroutine("FallAttack");
                }
            }
            else if(Input.GetMouseButton(0) && !fallAttack)
            {
                if(kickFallAttackTimer <= kickFallAttackTime)
                {
                    kickFallAttackTimer += Time.deltaTime;
                }
                else
                {
                    fallAttackVer = 1;
                    StartCoroutine("FallAttack");
                }
            }
            else
            {
                kickFallAttackTimer = 0f;
                cutterFallAttackTimer = 0f;
            }
        }
    }

    void fallAttackCollisionCheck()
    {
        GameObject circleChecker;
        if(fallAttackVer == 1)
        {
            circleChecker = Instantiate(preCircle, transform.position, Quaternion.identity);
            circleChecker.transform.localScale = new Vector3(circleRange[scrEvo.EvolutionNum], 0.1f, circleRange[scrEvo.EvolutionNum]);
            Destroy(circleChecker, 0.5f);
        }
    }

    IEnumerator FallAttack()
    {
        if(fallAttackVer == 1)
        {
            scrAnim.SetAnimator(Transition_R.Anim.KICKFA, true);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * scrEvo.Status_JUMP * 0.75f, ForceMode.Impulse);
            fallAttack = true;
            yield return new WaitForSeconds(0.95f);

            audioSource.PlayOneShot(KickFAClip);
            rb.AddForce(Vector3.down * jumpSpeed * 2f, ForceMode.Impulse);
            yield break;
        }
        else
        {
            scrAnim.SetAnimator(Transition_R.Anim.CUTTERFA, true);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * scrEvo.Status_JUMP * 0.75f, ForceMode.Impulse);
            fallAttack = true;
            yield return new WaitForSeconds(0.95f);

            audioSource.PlayOneShot(CutterFAClip);
            scrCutter.CutterAttack();
            rb.AddForce(Vector3.down * jumpSpeed * 2f, ForceMode.Impulse);
            yield break;
        }
    }

    public void FieldCheck(bool check)
    {
        //isGrounded = check;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position - transform.up * 0.05f, new Vector3(raycastCubeX[scrEvo.EvolutionNum], 0.1f ,raycastCubeZ[scrEvo.EvolutionNum]));
    }
}
