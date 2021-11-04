using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCameraJC_R : MonoBehaviour
{
    [SerializeField] GameObject objPlayer;
    [SerializeField] Transform target;
    [SerializeField] Transform[] focus;
    [SerializeField] float[] radius;
    [SerializeField] float[] EvolutionCamWorkHeight;
    [SerializeField] float[] EvolutionCamWorkMinRadius;
    [SerializeField] float[] EvolutionCamWorkMaxRadius;
    [SerializeField] float spinSpeed = 1.0f;
    [Header("カメラの振動"), SerializeField] float duration;
    [Tooltip("最大振幅の設定(進化段階ごと)"), SerializeField] private float[] magnitude;

    Vector3 nowPos;
    Vector3 pos = Vector3.zero;
    Vector2 mouse = Vector2.zero;
    Vector3 camPos;

    public bool evolutionAnimStart = false;
    public bool evolved = false;
    private float endEvolution = 0.25f;
    //M
    public float minY;

    // モバイル用
    private int camFingerId;
    private int ignoreFingerId;

    private enum eCamWork
    {
        eNormal,
        eEvolution,
    }

    private eCamWork eCameraWork = eCamWork.eNormal;

    private EvolutionChicken_R scrEvo;

    // Mobile Setting
    private bool mobileMode;

    //山本追加　カメラ感度操作用
    private SaveManager_Y saveManager;

    // Use this for initialization
    void Start()
    {
        // Camera get Start Position from Player
        nowPos = transform.position;

        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
            Debug.Log("player didn't setting. Auto search 'Player' tag.");
        }

        mouse.y = 0.5f; // start mouse y pos ,0.5f is half

        scrEvo = objPlayer.GetComponent<EvolutionChicken_R>();

        mobileMode = SaveManager_Y.GetInstance().isMobile;

        if (mobileMode)
        {
            camFingerId = -1;
            ignoreFingerId = -1;
        }

        //山本追加 セーブデータとマウス感度(spinSpeed)を連動させられるように
        var saveObj = GameObject.FindGameObjectWithTag("SaveManager");
        if (saveObj != null) saveManager = saveObj.GetComponent<SaveManager_Y>();
    }

    // Update is called once per frame
    void Update()
    {
        if (saveManager != null)
            SetSpinSpeed(saveManager.GetCameraSensitive());

        switch (eCameraWork)
        {
            case eCamWork.eNormal:
                // Get MouseMove
                if (!mobileMode)
                {
                    mouse -= new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
                }
                else
                {
                    // カメラのFingerIdを設定
                    if (camFingerId < 0)
                    {
                        foreach (Touch touch in Input.touches)
                        {
                            if ((touch.position.x > Screen.width * 0.20f &&
                               touch.position.y > Screen.height * 0.20f) &&
                               ignoreFingerId != touch.fingerId)
                            {
                                Debug.Log("CAMERAON");
                                camFingerId = touch.fingerId;
                            }
                        }
                    }

                    if (ignoreFingerId < 0)
                    {
                        foreach (Touch touch in Input.touches)
                        {
                            if ((touch.position.x < Screen.width * 0.20f &&
                               touch.position.y < Screen.height * 0.20f) &&
                               camFingerId != touch.fingerId)
                            {
                                Debug.Log("IGNORE");
                                ignoreFingerId = touch.fingerId;
                            }
                        }
                    }

                    // 実際のカメラ操作の実装
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.fingerId == camFingerId && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                            camFingerId = -1;

                        if (touch.fingerId == ignoreFingerId && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                            ignoreFingerId = -1;

                        if (camFingerId >= 0)
                        {
                            if (camFingerId == touch.fingerId)
                            {
                                Vector2 position = touch.deltaPosition;
                                mouse -= position * 0.1f * Time.deltaTime * spinSpeed;
                            }
                        }
                    }
                }

                // Clamp mouseY move
                mouse.y = Mathf.Clamp(mouse.y, 0.25f, 0.95f);
                //M
                //mouse.y = Mathf.Clamp(mouse.y, minY, 0.95f);

                // sphere coordinates
                pos.x = Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Cos(mouse.x * Mathf.PI);
                pos.y = Mathf.Cos(mouse.y * Mathf.PI);
                pos.z = Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Sin(mouse.x * Mathf.PI);

                //SetRadius
                pos *= radius[scrEvo.EvolutionNum];

                // r and upper
                pos *= nowPos.z;

                pos.y += nowPos.y;
                //pos.x += nowPos.x; // if u need a formula,pls remove comment tag.

                camPos = pos + focus[scrEvo.EvolutionNum].position;

                if (endEvolution < 0.25f)
                {
                    endEvolution += Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, camPos, 0.25f);
                }
                else
                {
                    transform.position = camPos;
                }
                transform.LookAt(focus[scrEvo.EvolutionNum]);
                SetCam();
                break;

            // 進化中のモーション(何も起こさない)
            case eCamWork.eEvolution:

                break;
        }
    }

    void SetCam()
    {
        Vector3 setCamPos;
        Vector3 distance = camPos - focus[scrEvo.EvolutionNum].position;
        Ray ray = new Ray(focus[scrEvo.EvolutionNum].position, distance);
        Debug.DrawRay(ray.origin, ray.direction * distance.magnitude, Color.red, 0.1f, false);
        RaycastHit[] hits = Physics.RaycastAll(ray, distance.magnitude);

        // カメラとチキン間に障害物が無かったら処理を終了する
        if (hits == null)
            return;

        // 順次処理を行う(Shard は無視する)
        foreach (var hit in hits)
        {
            //山本追加 餌でカメラがくがくしてたのを回避
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Food")) return;
            if (hit.transform.gameObject.layer != 10 && hit.transform.gameObject.layer != 17)
            {
                setCamPos = hit.point + transform.forward;
                transform.position = setCamPos;
                return;
            }
        }
        /*if(Physics.Raycast(ray, out RaycastHit hit, distance.magnitude) == true)
        {
            // レイヤーがShardの場合は貫通
            if(hit.transform.gameObject.layer != 8)
            {
                setCamPos = hit.point;
                transform.position = setCamPos;
            }
        }*/
    }

    public void Shake()
    {
        StartCoroutine(DoShake(duration, magnitude[scrEvo.EvolutionNum]));
    }

    private IEnumerator DoShake(float dur, float magnitude)
    {
        var elapsed = 0f;

        while (elapsed < dur)
        {
            var pos = transform.localPosition;
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator CameraWorkEvolution()
    {
        var timer = 0f;
        var camWorkPos = transform.position;
        float angle = objPlayer.transform.eulerAngles.y * Mathf.Deg2Rad;

        eCameraWork = eCamWork.eEvolution;
        evolutionAnimStart = true;

        while (timer < 5.0f)
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0.1f;

                if (timer <= 0.25f)                                     // カメラを所定の位置に移動
                {
                    transform.position = Vector3.Lerp(camWorkPos, Vector3.up * EvolutionCamWorkHeight[scrEvo.EvolutionNum] + objPlayer.transform.right * EvolutionCamWorkMinRadius[scrEvo.EvolutionNum] + focus[scrEvo.EvolutionNum].position, timer * 4);
                }
                else if (timer >= 0.25f && timer <= 2.25f)               // カメラズーム
                {
                    var distance = Vector3.Lerp(Vector3.right * (scrEvo.EvolutionNum + 1), Vector3.zero, (timer - 0.25f) / 2.0f);
                    transform.position = Vector3.up * EvolutionCamWorkHeight[scrEvo.EvolutionNum] + objPlayer.transform.position + objPlayer.transform.right * (EvolutionCamWorkMinRadius[scrEvo.EvolutionNum] + distance.x);
                }
                else if (timer > 2.5f && timer <= 5.0f)
                {
                    var distance = new Vector3(0.0f, 0.0f, 0.0f);       // カメラを引く
                    if (timer <= 2.75f)
                    {
                        distance = Vector3.Lerp(distance, Vector3.right, (timer - 2.5f) / 0.25f);
                    }
                    else
                    {
                        distance = new Vector3(1.0f, 0.0f, 0.0f);
                    }

                    angle += Mathf.PI / 2.5f * Time.unscaledDeltaTime;
                    transform.position = objPlayer.transform.position + new Vector3(Mathf.Cos(angle), 1.0f, Mathf.Sin(angle)) * (EvolutionCamWorkMinRadius[scrEvo.EvolutionNum] + distance.x * EvolutionCamWorkMinRadius[scrEvo.EvolutionNum]);     // カメラを回転させる
                }

                transform.LookAt(objPlayer.transform);
                timer += Time.unscaledDeltaTime;
            }
            yield return null;
        }
        // 進化フラグを設定
        evolved = true;

        Time.timeScale = 1.0f;
        endEvolution = 0.0f;
        eCameraWork = eCamWork.eNormal;
    }

    //山本追加 カメラ感度設定関数
    public void SetSpinSpeed(float sensitive)
    {
        spinSpeed = sensitive;
        if (spinSpeed < 0f) spinSpeed = 0f;
        else if (spinSpeed > 1f) spinSpeed = 1f;
    }
}
