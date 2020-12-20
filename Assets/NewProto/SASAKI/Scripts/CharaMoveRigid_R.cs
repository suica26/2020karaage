using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMoveRigid_R : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpSpeed = 3f;
    [SerializeField] private float rotateSpeed = 1.5f;
    [SerializeField] private GameObject preBlock = null;

    private Rigidbody rb;
    private float h, v;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded = false;
    private Ray ray;

    //落下攻撃用変数
    private bool fallAttack = false;

    private GameObject objFallAttack = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //足元から下へ向けて球状のRayを発射し，着地判定をする
        ray = new Ray(gameObject.transform.position + 0.15f * gameObject.transform.up, - gameObject.transform.up);
        isGrounded = Physics.SphereCast(ray, 0.13f, 0.08f);
        //着地判定の範囲をシーンに示す
        Debug.DrawRay(gameObject.transform.position + 0.2f * gameObject.transform.up, -0.22f * gameObject.transform.up);

        if (isGrounded)
        {
            if (fallAttack)
            {
                fallImpact();
                fallAttack = false;
            }

            if (h != 0 || v != 0)
            {
                moveDirection = speed * new Vector3(h, 0, v);
                    
                Quaternion qua = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, rotateSpeed * Time.deltaTime);

                //moveDirection = transform.TransformDirection(moveDirection);
                rb.velocity = moveDirection;
            }

            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector3(rb.velocity.x, 5 * jumpSpeed, rb.velocity.z);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q) && !fallAttack)
            {
                rb.AddForce(Vector3.down * jumpSpeed * 10f, ForceMode.VelocityChange);
                fallAttack = true;
            }
        }

    }

    private void fallImpact()
    {
        objFallAttack = preBlock;
        Instantiate(objFallAttack, gameObject.transform);
    }
}
