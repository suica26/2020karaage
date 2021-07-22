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

    private enum eCamWork
    {
        eNormal,
        eEvolution,
    }

    private eCamWork eCameraWork = eCamWork.eNormal;

    private EvolutionChicken_R scrEvo;

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
    }

    // Update is called once per frame
    void Update()
    {
        switch(eCameraWork)
        {
            case eCamWork.eNormal:
                // Get MouseMove
                mouse -= new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
                // Clamp mouseY move
                //mouse.y = Mathf.Clamp(mouse.y, 0.25f, 0.95f);
                //M
                mouse.y = Mathf.Clamp(mouse.y, minY, 0.95f);

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

                if(endEvolution < 0.25f)
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
        Debug.DrawRay(ray.origin, ray.direction * distance.magnitude, Color.red, 0.1f,false);
        if(Physics.Raycast(ray, out RaycastHit hit, distance.magnitude) == true)
        {
            setCamPos = hit.point;
            transform.position = setCamPos;
        }
    }

    public void Shake()
    {
        StartCoroutine(DoShake(duration, magnitude[scrEvo.EvolutionNum]));
    }

    private IEnumerator DoShake(float dur, float magnitude)
    {
        var elapsed = 0f;

        while(elapsed < dur)
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
            if(Time.timeScale != 0)
            {
                Time.timeScale = 0.1f;

                if (timer <= 0.25f)                                     // カメラを所定の位置に移動
                {
                    transform.position = Vector3.Lerp(camWorkPos, Vector3.up * EvolutionCamWorkHeight[scrEvo.EvolutionNum] + objPlayer.transform.right * EvolutionCamWorkMinRadius[scrEvo.EvolutionNum] + focus[scrEvo.EvolutionNum].position, timer * 4);
                }
                else if (timer >= 0.25f && timer <= 0.8f)               // カメラズーム
                {
                    var distance = Vector3.Lerp(Vector3.right * (scrEvo.EvolutionNum + 1), Vector3.zero, (timer - 0.25f) / 0.75f);
                    transform.position = Vector3.up * EvolutionCamWorkHeight[scrEvo.EvolutionNum] + objPlayer.transform.position + objPlayer.transform.right * (EvolutionCamWorkMinRadius[scrEvo.EvolutionNum] + distance.x);
                }
                else if (timer > 1.0f && timer <= 5.0f)
                {
                    // 進化フラグを設定
                    evolved = true;

                    var distance = new Vector3(0.0f, 0.0f, 0.0f);       // カメラを引く
                    if (timer <= 1.25f)
                    {
                        distance = Vector3.Lerp(distance, Vector3.right, (timer - 1.0f) / 0.25f);
                    }
                    else
                    {
                        distance = new Vector3(1.0f, 0.0f, 0.0f);
                    }

                    angle += Mathf.PI / 4.0f * Time.unscaledDeltaTime;
                    transform.position = objPlayer.transform.position + new Vector3(Mathf.Cos(angle), 1.0f, Mathf.Sin(angle)) * (EvolutionCamWorkMinRadius[scrEvo.EvolutionNum] + distance.x * EvolutionCamWorkMinRadius[scrEvo.EvolutionNum]);     // カメラを回転させる
                }

                transform.LookAt(objPlayer.transform);
                timer += Time.unscaledDeltaTime;
            }
            yield return null;
        }
        Time.timeScale = 1.0f;
        endEvolution = 0.0f;
        eCameraWork = eCamWork.eNormal;
    }
}
