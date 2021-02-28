using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMoveRigid_R : MonoBehaviour
{
    [SerializeField] private AudioClip[] walkClip;
    [Tooltip("足音用AudioSource"), SerializeField] private AudioSource audioSourceWalk;
    [Tooltip("ジャンプ、落下攻撃用"), SerializeField] private AudioSource audioSourceCommon;
    [Tooltip("キャラクターの回転速度"), SerializeField] private float rotateSpeed;

    [Header("接地判定用")]
    [Tooltip("BoxCast(足元検知用)のX成分指定(1~4段階まで)"), SerializeField] private float[] raycastCubeX;
    [Tooltip("BoxCast(足元検知用)のZ成分指定(1~4段階まで)"), SerializeField] private float[] raycastCubeZ;

    [Header("落下攻撃設定")]
    [Tooltip("落下攻撃時の衝撃波の半径"), SerializeField] private float[] circleRange;
    [SerializeField] GameObject preCircle;
    [SerializeField] private AudioClip CutterFAClip;
    [SerializeField] private AudioClip KickFAClip;
    [SerializeField] private AudioClip JumpClip;

    [Header("アニメーション処理用")]
    [SerializeField] private Transition_R scrAnim;

    private Rigidbody rb;
    private float h, v;
    private Vector3 cameraForward = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded = true;

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
            //Debug.Log(hit.transform.gameObject.name);
            if(hit.transform.gameObject.tag != "Player")
            {
                isGrounded = true;
            }
        }

        //以下接地時の処理を記述
        if (isGrounded)
        {
            scrAnim.SetAnimator(Transition_R.Anim.JUMP, false);

            //FallAttack中に接地した際の処理
            if (fallAttack)
            {
                scrAnim.SetAnimator(Transition_R.Anim.KICKFA, false);
                scrAnim.SetAnimator(Transition_R.Anim.CUTTERFA, false);
                fallAttack = false;
                fallAttackCollisionCheck();
                rb.velocity = Vector3.zero;
            }

            //チキンが移動している際の処理
            if (h != 0 || v != 0)
            {
                //足音が再生されていない場合に足音を再生
                if(audioSourceWalk.isPlaying != walkClip[scrEvo.EvolutionNum])
                {
                    audioSourceWalk.PlayOneShot(walkClip[scrEvo.EvolutionNum]);
                }
                scrAnim.SetAnimator(Transition_R.Anim.WALK, true);

                //チキンの移動方向や移動量を計算
                cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                moveDirection = cameraForward * v + Camera.main.transform.right * h;
                if (moveDirection.magnitude > 1)
                {
                    moveDirection.Normalize();
                }

                rb.velocity = moveDirection * speed;

                //チキンの身体の向きを修正
                if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                {
                    Quaternion qua = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, rotateSpeed * Time.deltaTime);
                }
            }
            else
            {
                //足音が再生されていた場合停止させる
                if (audioSourceWalk.isPlaying == walkClip[scrEvo.EvolutionNum])
                {
                    audioSourceWalk.Stop();
                }
                scrAnim.SetAnimator(Transition_R.Anim.WALK, false);
            }

            //ジャンプした際の処理
            if (Input.GetButtonDown("Jump"))
            {
                audioSourceCommon.PlayOneShot(JumpClip);
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
                if (audioSourceWalk.isPlaying == walkClip[scrEvo.EvolutionNum])
                {
                    audioSourceWalk.Stop();
                }

                cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                moveDirection = cameraForward * v + Camera.main.transform.right * h;
                rb.AddForce(moveDirection * speed * 0.33f, ForceMode.Force);

                var velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                if(velocity.magnitude > speed)
                {
                    rb.velocity = velocity.normalized * speed + rb.velocity.y * Vector3.up;
                }
            }

            //落下攻撃の処理(キック)
            if(Input.GetMouseButton(0) && !fallAttack)
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
            //落下攻撃の処理(カッター)
            else if (Input.GetMouseButton(1) && !fallAttack)
            {
                if (cutterFallAttackTimer <= cutterFallAttackTime)
                {
                    cutterFallAttackTimer += Time.deltaTime;
                }
                else if (!scrCutter.throwingCutter)
                {
                    fallAttackVer = 2;
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

    //落下攻撃後、接地時にInvoke。衝撃波を設定
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

    //落下攻撃時の動きの処理
    IEnumerator FallAttack()
    {
        //キック
        if(fallAttackVer == 1)
        {
            scrAnim.SetAnimator(Transition_R.Anim.KICKFA, true);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * scrEvo.Status_JUMP * 0.75f, ForceMode.Impulse);
            fallAttack = true;
            yield return new WaitForSeconds(0.95f);

            audioSourceCommon.PlayOneShot(KickFAClip);
            rb.AddForce(Vector3.down * jumpSpeed * 4f, ForceMode.Impulse);
            yield break;
        }
        //カッター
        else
        {
            scrAnim.SetAnimator(Transition_R.Anim.CUTTERFA, true);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * scrEvo.Status_JUMP * 0.75f, ForceMode.Impulse);
            fallAttack = true;
            yield return new WaitForSeconds(0.95f);

            audioSourceCommon.PlayOneShot(CutterFAClip);
            scrCutter.CutterAttack();
            rb.AddForce(Vector3.down * jumpSpeed * 2f, ForceMode.Impulse);
            yield break;
        }
    }
}
