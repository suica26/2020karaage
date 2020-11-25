using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCameraJC_R : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float spinSpeed = 1.0f;

    Vector3 nowPos;
    Vector3 pos = Vector3.zero;
    Vector2 mouse = Vector2.zero;
    Vector3 camPos;

    // Use this for initialization
    void Start()
    {
        // Canera get Start Position from Player
        nowPos = transform.position;

        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
            Debug.Log("player didn't setting. Auto search 'Player' tag.");
        }

        mouse.y = 0.5f; // start mouse y pos ,0.5f is half
    }

    // Update is called once per frame
    void Update()
    {

        // Get MouseMove
        mouse -= new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
        // Clamp mouseY move
        mouse.y = Mathf.Clamp(mouse.y, 0.5f, 0.95f);

        // sphere coordinates
        pos.x = Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Cos(mouse.x * Mathf.PI);
        pos.y = Mathf.Cos(mouse.y * Mathf.PI);
        pos.z = Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Sin(mouse.x * Mathf.PI);
        // r and upper
        pos *= nowPos.z;

        pos.y += nowPos.y;
        //pos.x += nowPos.x; // if u need a formula,pls remove comment tag.

        camPos = pos + target.position;
        transform.position = camPos;
        transform.LookAt(target.transform);
        SetCam();
    }

    void SetCam()
    {
        Vector3 setCamPos;
        Vector3 distance = camPos - target.transform.position;
        Ray ray = new Ray(target.transform.position, distance);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 0.1f,false);
        if(Physics.Raycast(ray, out RaycastHit hit, distance.magnitude) == true)
        {
            setCamPos = hit.point;
            transform.position = setCamPos;
        }
    }
}
