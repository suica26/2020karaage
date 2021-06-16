using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCameraJC_R : MonoBehaviour
{
    [SerializeField] GameObject objPlayer;
    [SerializeField] Transform target;
    [SerializeField] Transform[] focus;
    [SerializeField] float[] radius;
    [SerializeField] float spinSpeed = 1.0f;
    [Header("カメラの振動"), SerializeField] float duration;
    [Tooltip("最大振幅の設定(進化段階ごと)"), SerializeField] private float[] magnitude;

    Vector3 nowPos;
    Vector3 pos = Vector3.zero;
    Vector2 mouse = Vector2.zero;
    Vector3 camPos;

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

        // Get MouseMove
        mouse -= new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
        // Clamp mouseY move
        mouse.y = Mathf.Clamp(mouse.y, 0.25f, 0.95f);

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
        transform.position = camPos;
        transform.LookAt(focus[scrEvo.EvolutionNum]);
        SetCam();
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
}
