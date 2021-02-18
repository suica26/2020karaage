using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterMove1_R : MonoBehaviour
{
    [SerializeField] private AudioClip cutterSound;
    [SerializeField] private float rotSpeed;

    private float destroyTime = 3.4f;

    private GameObject player = null;
    private AudioSource audioSource = null;
    private Rigidbody rigid = null;

    private Vector3 moveVec;
    public Transform backArea;
    public float evoSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moveVec = player.transform.forward;

        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.AddForce(moveVec * 12f * evoSpeed, ForceMode.Impulse);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(cutterSound);
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime >= 1.7f)
        {
            rigid.AddForce(-moveVec * 350f * evoSpeed * Time.deltaTime, ForceMode.Force);   
        }
        else if (destroyTime < 1.7f)
        {
            transform.position = Vector3.MoveTowards(transform.position, backArea.position - player.transform.forward, 30f * evoSpeed * Time.deltaTime);    
        }
        gameObject.transform.Rotate(rotSpeed * Time.deltaTime, 0, 0);
    }
}
