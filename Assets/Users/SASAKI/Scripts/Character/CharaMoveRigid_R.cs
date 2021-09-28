using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMoveRigid_R : MonoBehaviour
{
    [Tooltip("キャラクターの回転速度"), SerializeField] private float rotateSpeed;
    [Tooltip("移動時のエフェクト"), SerializeField] private GameObject[] moveEffect;
    [Tooltip("グライド時のエフェクト"), SerializeField] private GameObject[] glideEffect;

    [Header("接地判定用")]
    [Tooltip("BoxCast(足元検知用)のX成分指定(1~4段階まで)"), SerializeField] private float[] raycastCubeX;
    [Tooltip("BoxCast(足元検知用)のY成分指定(1~4段階まで)"), SerializeField] private float[] raycastCubeY;
    [Tooltip("BoxCast(足元検知用)のZ成分指定(1~4段階まで)"), SerializeField] private float[] raycastCubeZ;

    [Header("落下攻撃設定")]
    [Tooltip("落下攻撃時の衝撃波の半径"), SerializeField] private float[] circleRange;
    [Tooltip("各進化段階の落下攻撃の威力UP(1 => 2)"), SerializeField] private float[] fallAttackFirstHeight;
    [Tooltip("各進化段階の落下攻撃の威力UP(2 => 3)"), SerializeField] private float[] fallAttackSecondHeight;
    [Tooltip("落下攻撃の倍率設定"), SerializeField] private float[] boostMag;
    [SerializeField] GameObject preCircle;
    [SerializeField] private GameObject fallAttackKickEffect;

    [Header("混乱用")]
    [SerializeField] private float[] effectScale;
    [SerializeField] GameObject confuseEffect;

    [Header("アニメーション処理用")]
    [SerializeField] private Transition_R[] scrAnim;

    private Rigidbody rb;
    private float h, v;
    private Vector3 cameraForward = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded = true;

    private TpsCameraJC_R scrCam;
    private EvolutionChicken_R scrEvo;
    private Cutter_R scrCutter;
    private float speed;
    private float jumpSpeed;

    //落下攻撃用変数
    private bool fallAttack = false;
    private int fallAttackVer = 0;

    private float kickFallAttackTimer = 0f;
    private float kickFallAttackTime = 0.3f;
    private float cutterFallAttackTimer = 0f;
    private float cutterFallAttackTime = 0.3f;

    //落下攻撃威力変動用
    private float startHeight;
    private float endHeight;
    public float damageBoost;

    private bool isFlying;
    public bool _isFlying { get { return isFlying; } set { isFlying = value; } }

    // 行動可能か判定
    public bool stunFlag;
    private bool stunned;
    private float timer;

    // ブラストの移動制御用変数
    public float MoveMag { get; set; }

    //ADX
    private new CriAtomSource audio;
    CriAtomExPlayback JumpS, WalkVoiceS, FootS, IdleVoiceS;
    //加筆　undertreem 0628
    private ADX_SoundRaycast ADX_RevLevel_L, ADX_RevLevel_R;
    private float BusLevel_L, BusLevel_R;

    // Mobile Operation
    private bool mobileMode;
    private Joystick joystick;
    private bool pushJumpButton;
    private bool pushKickButton;
    private bool pushCutterButton;

    // 坂道移動用
    private Vector3 inputVector;
    private Vector3 normalVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scrCam = Camera.main.GetComponent<TpsCameraJC_R>();
        scrEvo = GetComponent<EvolutionChicken_R>();
        scrCutter = GetComponent<Cutter_R>();
        MoveMag = 1.0f;
        isFlying = false;
        stunned = false;
        audio = (CriAtomSource)GetComponent("CriAtomSource");
        //加筆　undertreem 0628
        ADX_RevLevel_L = GetComponent<ADX_SoundRaycast>();
        ADX_RevLevel_R = GetComponent<ADX_SoundRaycast>();
        //山本加筆 BusLevelがStage0でNullException吐きまくってたので、初期化付けました

        mobileMode = MobileSetting_R.GetInstance().IsMobileMode();
        if (mobileMode)
        {
            joystick = FindObjectOfType<Joystick>();
            pushJumpButton = false;
            pushKickButton = false;
            pushCutterButton = false;
        }
    }

    void Update()
    {
        // 攻撃判定の更新
        AttackRestrictions_R.GetInstance().Update();

        //M 追加:時間停止中は操作を遮断
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        speed = scrEvo.Status_SPD;
        jumpSpeed = scrEvo.Status_JUMP;

        if(!mobileMode)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }
        else
        {
            h = joystick.Horizontal;
            v = joystick.Vertical;
        }

        if(Camera.main.GetComponent<TpsCameraJC_R>().evolutionAnimStart)
        {
            Camera.main.transform.GetComponent<TpsCameraJC_R>().evolutionAnimStart = false;
            scrAnim[scrEvo.EvolutionNum-1].SetAnimator(Transition_R.Anim.EVOLUTION, true);
        }

        /*
        //足元から下へ向けて球状のRayを発射し，着地判定をする
        ray = new Ray(gameObject.transform.position + 0.15f * gameObject.transform.up, - gameObject.transform.up);
        isGrounded = Physics.SphereCast(ray, 0.13f, 0.08f);
        //着地判定の範囲をシーンに示す
        Debug.DrawRay(gameObject.transform.position + 0.2f * gameObject.transform.up, -0.22f * gameObject.transform.up);
        */

        //BoxCastで設置判定
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + transform.up * 0.1f, new Vector3(raycastCubeX[scrEvo.EvolutionNum], raycastCubeY[scrEvo.EvolutionNum], raycastCubeZ[scrEvo.EvolutionNum]) * 0.5f, -transform.up,
                           Quaternion.Euler(transform.rotation.eulerAngles), 0.1f);
        isGrounded = false;

        foreach (RaycastHit hit in hits)
        {
            //Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.tag != "Player")
            {
                isGrounded = true;
                normalVector = hit.normal;
                break;
            }
        }
        // もしスタン中なら何もしない。
        if(stunned)
            return;

        //以下接地時の処理を記述
        if (isGrounded)
        {
            if(stunFlag)
            {
                stunFlag = false;
                StartCoroutine("StunnedChicken");
                return;
            }

            // もし非アクティブなら、移動時のエフェクトをアクティブにする
            if (moveEffect[scrEvo.EvolutionNum].GetComponent<ParticleSystem>().startLifetime != 2)
                moveEffect[scrEvo.EvolutionNum].GetComponent<ParticleSystem>().startLifetime = 2;

            if (glideEffect[scrEvo.EvolutionNum].GetComponent<ParticleSystem>().maxParticles != 0)
            {
                glideEffect[scrEvo.EvolutionNum].GetComponent<ParticleSystem>().maxParticles = 0;
                glideEffect[scrEvo.EvolutionNum].transform.GetChild(0).GetComponent<ParticleSystem>().maxParticles = 0;
            }


            // 滑空の接地時の処理
            if (isFlying)
            {
                scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.GLIDING, false);
                rb.useGravity = true;
                isFlying = false;
            }

            scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.JUMP, false);

            //FallAttack中に接地した際の処理
            if (fallAttack)
            {
                endHeight = transform.position.y; //着地時の高さを取得
                scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.KICKFA, false);
                scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.CUTTERFA, false);
                fallAttack = false;
                FallAttackCollisionCheck();
                rb.velocity = Vector3.zero;
            }

            //チキンが移動している際の処理
            if (h != 0 || v != 0)
            {
                BusLevel_L = ADX_RevLevel_L.ADX_BusSendLevel_L;
                BusLevel_R = ADX_RevLevel_R.ADX_BusSendLevel_R;
                //警告出てるけど逆にこれ以外だとエラー吐くので現状このまま
                audio.SetBusSendLevelOffset(2, BusLevel_L);
                audio.SetBusSendLevelOffset(3, BusLevel_R);
                FootS = audio.Play("FootSteps");
                WalkVoiceS = audio.Play("Walk_Voice00");


                scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.WALK, true);

                //チキンの移動方向や移動量を計算
                cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                moveDirection = Vector3.ProjectOnPlane(cameraForward * v + Camera.main.transform.right * h, normalVector);
                if (moveDirection.magnitude > 1)
                {
                    moveDirection.Normalize();
                }

                rb.velocity = moveDirection * speed * MoveMag;

                //チキンの身体の向きを修正
                if (!mobileMode)
                {
                    if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                    {
                        Quaternion qua = Quaternion.LookRotation(moveDirection);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, rotateSpeed * Time.deltaTime);
                    }
                }
                else
                {
                    if(joystick.Horizontal != 0 || joystick.Vertical != 0)
                    {
                        Quaternion qua = Quaternion.LookRotation(moveDirection);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, rotateSpeed * Time.deltaTime);
                    }
                }
            }
            else
            {
                scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.WALK, false);
                WalkVoiceS.Stop();
                IdleVoiceS = audio.Play("Idle_Voice00");
            }

            //ジャンプした際の処理
            if (!mobileMode)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    audio.Stop();
                    JumpS = audio.Play("Jump");
                    scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.JUMP, true);
                    rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                }
            }
            else
            {
                if(pushJumpButton)
                {
                    audio.Stop();
                    JumpS = audio.Play("Jump");
                    scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.JUMP, true);
                    rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                    pushJumpButton = false;
                }
            }
        }
        //空中の処理を記述
        else
        {
            if (isFlying == false)
            {
                scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.GLIDING, false);
            }

            scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.JUMP, true);
            scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.WALK, false);

            // もしアクティブなら移動時のエフェクトを非アクティブにする
            if (moveEffect[scrEvo.EvolutionNum].GetComponent<ParticleSystem>().startLifetime != 0)
                moveEffect[scrEvo.EvolutionNum].GetComponent<ParticleSystem>().startLifetime = 0;
            //moveEffect[scrEvo.EvolutionNum].SetActive(false);

            //空中での制動(移動量は地上の1/3程度)
            if (h != 0 || v != 0)
            {
                IdleVoiceS.Stop();
                cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                moveDirection = cameraForward * v + Camera.main.transform.right * h;

                if (isFlying)
                {
                    if (rb.velocity.y < 0)
                    {
                        rb.useGravity = false;
                        rb.AddForce(Vector3.down * 9.81f * 0.25f, ForceMode.Acceleration);

                        if (glideEffect[scrEvo.EvolutionNum].GetComponent<ParticleSystem>().maxParticles != 100)
                        {
                            glideEffect[scrEvo.EvolutionNum].GetComponent<ParticleSystem>().maxParticles = 100;
                            glideEffect[scrEvo.EvolutionNum].transform.GetChild(0).GetComponent<ParticleSystem>().maxParticles = 100;
                        }
                    }
                    rb.AddForce(moveDirection * speed * 1.5f, ForceMode.Force);

                    Quaternion qua = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, rotateSpeed * Time.deltaTime);
                }
                else
                    rb.AddForce(moveDirection * speed * 0.33f, ForceMode.Force);

                var velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                if (velocity.magnitude > speed)
                {
                    rb.velocity = velocity.normalized * speed * MoveMag + rb.velocity.y * Vector3.up;
                }
            }
            else
            {
                if (isFlying)
                {
                    scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.GLIDING, true);
                    rb.useGravity = true;
                }
            }

            //落下攻撃の処理(キック & カッター)
            if(!mobileMode)
            {
                if (Input.GetMouseButton(0) && !fallAttack)
                {
                    if (kickFallAttackTimer <= kickFallAttackTime)
                        kickFallAttackTimer += Time.deltaTime;
                    else
                    {
                        fallAttackVer = 1;
                        StartCoroutine("FallAttack");
                    }
                }
                else if (Input.GetMouseButton(1) && !fallAttack && scrCutter.enabled)
                {
                    if (cutterFallAttackTimer <= cutterFallAttackTime)
                        cutterFallAttackTimer += Time.deltaTime;
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
            else
            {
                if(pushKickButton && !fallAttack)
                {
                    if (kickFallAttackTimer <= kickFallAttackTime)
                        kickFallAttackTimer += Time.deltaTime;
                    else
                    {
                        fallAttackVer = 1;
                        StartCoroutine("FallAttack");
                    }
                }
                else if (pushCutterButton && !fallAttack && scrCutter.enabled)
                {
                    if (cutterFallAttackTimer <= cutterFallAttackTime)
                        cutterFallAttackTimer += Time.deltaTime;
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
            IdleVoiceS.Stop();
        }
    }

    //落下攻撃後、接地時にInvoke。衝撃波を設定
    void FallAttackCollisionCheck()
    {
        GameObject circleChecker;
        if (fallAttackVer == 1)
        {
            audio.Play("FallShock");
            //落下攻撃のダメージ上昇量を導出
            if (startHeight - endHeight < fallAttackFirstHeight[scrEvo.EvolutionNum])
                damageBoost = boostMag[0];
            else if (startHeight - endHeight > fallAttackFirstHeight[scrEvo.EvolutionNum] && startHeight - endHeight < fallAttackSecondHeight[scrEvo.EvolutionNum])
                damageBoost = boostMag[1];
            else
                damageBoost = boostMag[2];
            Debug.Log(damageBoost);

            scrCam.Shake();
            if (fallAttackKickEffect != null)
            {
                GameObject effect = Instantiate(fallAttackKickEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.5f);
            }
            circleChecker = Instantiate(preCircle, transform.position, Quaternion.identity);
            circleChecker.transform.localScale = new Vector3(circleRange[scrEvo.EvolutionNum], 0.1f, circleRange[scrEvo.EvolutionNum]);
            Destroy(circleChecker, 0.5f);
        }
    }

    //落下攻撃時の動きの処理
    IEnumerator FallAttack()
    {
        if (isFlying)
        {
            scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.GLIDING, false);
            isFlying = false;
            rb.useGravity = true;
        }

        //キック
        if (fallAttackVer == 1)
        {
            startHeight = transform.position.y; //落下攻撃開始時の高さを取得
            scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.KICKFA, true);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * scrEvo.Status_JUMP * 0.75f, ForceMode.Impulse);
            fallAttack = true;
            yield return new WaitForSeconds(0.95f);

            //audioSourceCommon.PlayOneShot(KickFAClip);
            JumpS.Stop();
            audio.Play("FallAttack00");
            rb.AddForce(Vector3.down * jumpSpeed * 4f, ForceMode.Impulse);
            yield break;
        }
        //カッター
        else
        {
            scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.CUTTERFA, true);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * scrEvo.Status_JUMP * 0.75f, ForceMode.Impulse);
            fallAttack = true;
            yield return new WaitForSeconds(0.95f);

            //audioSourceCommon.PlayOneShot(CutterFAClip);
            JumpS.Stop();
            audio.Play("FallAttack00");
            scrCutter.CutterAttack();
            rb.AddForce(Vector3.down * jumpSpeed * 2f, ForceMode.Impulse);
            yield break;
        }
    }

    // スタン
    public IEnumerator StunnedChicken()
    {
        timer = 0.0f;
        stunned = true;
        //Sound
        WalkVoiceS.Stop();
        IdleVoiceS.Stop();
        audio.Play("Confusion");

        GameObject effect = Instantiate(confuseEffect, transform);
        if (effect.GetComponent<ParticleSystem>() != null)
            effect.GetComponent<ParticleSystem>().Play();

        //子オブジェクトのパーティクルを再生
        foreach (var childObj in effect.GetComponentsInChildren<ParticleSystem>())
        {
            childObj.Play();
        }
        Destroy(effect, 1.0f);
        effect.transform.localScale *= effectScale[scrEvo.EvolutionNum];

        while (timer < 1.0f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        stunned = false;
    }

    // Mobile Setting
    public void PushButton()
    {
        pushJumpButton = true;
    }

    public void PushKickButton()
    {
        pushKickButton = true;
    }
    public void PushCutterButton()
    {
        pushCutterButton = true;
    }

    public void ReleaseButton()
    {
        pushJumpButton = false;
    }
    public void ReleaseKickButton()
    {
        pushKickButton = false;
    }
    public void ReleaseCutterButton()
    {
        pushCutterButton = false;
    }
}
