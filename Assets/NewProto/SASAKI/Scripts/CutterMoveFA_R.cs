using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterMoveFA_R : MonoBehaviour
{
    [SerializeField] private AudioClip CutterClip;

    private GameObject player;
    private Rigidbody rigid;
    private AudioSource audioSource;

    private Vector3 moveVec;
    public Transform backArea;
    public float evoSpeed;
    public float cutterBaseSpeed;
    public float dropSpeed;

    private float rotSpeed = 360f;
    private float destroyTime;
    private bool touchGround;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moveVec = player.transform.forward;
        touchGround = false;
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(-transform.up * dropSpeed * evoSpeed, ForceMode.Impulse);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (touchGround)
        {
            destroyTime += Time.deltaTime;
            if (destroyTime <= 1.0f)
            {
                rigid.AddForce(-moveVec * cutterBaseSpeed * evoSpeed, ForceMode.Force);
            }
            else if (destroyTime > 1.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, backArea.position, cutterBaseSpeed * 2f * evoSpeed * Time.deltaTime);
            }
        }
        gameObject.transform.Rotate(rotSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enabled)
        {
            audioSource.PlayOneShot(CutterClip);
            if (other.gameObject.tag == "Ground")
            {
                touchGround = true;
                rigid.velocity = Vector3.zero;
                rigid.AddForce(moveVec * cutterBaseSpeed * evoSpeed, ForceMode.Impulse);
            }
        }
    }
}
