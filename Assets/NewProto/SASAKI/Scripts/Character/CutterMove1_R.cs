using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterMove1_R : MonoBehaviour
{
    [SerializeField] private AudioClip cutterSound;

    private float rotSpeed = 360f;
    private float destroyTime;

    private GameObject player;
    private AudioSource audioSource;
    private Rigidbody rigid;

    private Vector3 moveVec;
    public Transform backArea;
    public float evoSpeed;
    public float cutterBaseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moveVec = player.transform.forward;

        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.AddForce(moveVec * cutterBaseSpeed * evoSpeed, ForceMode.Impulse);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(cutterSound);
    }

    // Update is called once per frame
    void FixedUpdate()
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
        gameObject.transform.Rotate(rotSpeed * Time.deltaTime, 0, 0);
    }
}
