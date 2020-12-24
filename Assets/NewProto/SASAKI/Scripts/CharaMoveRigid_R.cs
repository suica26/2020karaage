using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMoveRigid_R : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpSpeed = 3f;
    [SerializeField] private float rotateSpeed = 1.5f;

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
                fallAttack = false;
                rb.velocity = Vector3.zero;
            }

            if (h != 0 || v != 0)
            {
                moveDirection = new Vector3(h, 0, v);
                if(moveDirection.magnitude > 1)
                {
                    moveDirection.Normalize();
                }

                rb.velocity = moveDirection * speed;

                Quaternion qua = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, rotateSpeed * Time.deltaTime);
            }

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }
        }
        else
        {
            //空中での制動(移動量は地上の1/3程度)
            if(h != 0 || v != 0)
            {
                moveDirection = new Vector3(h, 0, v);
                rb.AddForce(moveDirection * speed * 0.33f, ForceMode.Force);

                var velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                if(velocity.magnitude > speed)
                {
                    rb.velocity = velocity.normalized * speed + rb.velocity.y * Vector3.up;
                }
            }

            if (Input.GetButtonDown("Jump") && !fallAttack)
            {
                StartCoroutine("FallAttack");
            }
        }

    }

    IEnumerator FallAttack()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * 4f, ForceMode.Impulse);
        fallAttack = true;
        yield return new WaitForSeconds(0.5f);

        rb.AddForce(Vector3.down * jumpSpeed * 2f, ForceMode.Impulse);
        yield break;
    }
}
