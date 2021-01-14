using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMoveRigid_R : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpSpeed = 3f;
    [SerializeField] private float rotateSpeed = 1.5f;

    [Header("落下攻撃設定")]
    [SerializeField] private float circleRange;
    [SerializeField] private float circleKickRange;
    [SerializeField] private float addFanWidth;
    [SerializeField] private float addFanHeight;
    [SerializeField] GameObject preCircle;
    [SerializeField] GameObject preFan;

    private Rigidbody rb;
    private float h, v;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded = false;
    private Ray ray;

    //落下攻撃用変数
    private bool fallAttack = false;
    private int fallAttackVer = 0;

    private float kickFallAttackTimer = 0f;
    private float kickFallAttackTime = 0.5f;
    private float cutterFallAttackTimer = 0f;
    private float cutterFallAttackTime = 0.5f;

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
                fallAttackCollisionCheck(fallAttackVer);
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

                if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                {
                    Quaternion qua = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, rotateSpeed * Time.deltaTime);
                }
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

            if (Input.GetMouseButton(1) && !fallAttack)
            {
                if(cutterFallAttackTimer <= cutterFallAttackTime)
                {
                    cutterFallAttackTimer += Time.deltaTime;
                }
                else
                {
                    fallAttackVer = 1;
                    StartCoroutine("FallAttack");
                }
            }
            else if(Input.GetMouseButton(0) && !fallAttack)
            {
                if(kickFallAttackTimer <= kickFallAttackTime)
                {
                    kickFallAttackTimer += Time.deltaTime;
                }
                else
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

    }

    void fallAttackCollisionCheck(int kickOrCutter)
    {
        GameObject circleChecker;
        if(kickOrCutter == 1)
        {
            circleChecker = Instantiate(preCircle, transform.position, Quaternion.identity);
            circleChecker.transform.localScale = new Vector3(circleRange, 0.1f, circleRange);
            Destroy(circleChecker, 0.5f);

            GameObject fanChecker = Instantiate(preFan, transform.position + (transform.forward * addFanHeight / 2), Quaternion.identity);
            fanChecker.transform.rotation = transform.localRotation;
            fanChecker.transform.localScale = new Vector3(addFanWidth, 0.1f, addFanHeight);
            Destroy(fanChecker, 0.5f);
        }
        else if(kickOrCutter == 2)
        {
            circleChecker = Instantiate(preCircle, transform.position, Quaternion.identity);
            circleChecker.transform.localScale = new Vector3(circleKickRange, 0.1f, circleKickRange);
            Destroy(circleChecker, 0.5f);
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
