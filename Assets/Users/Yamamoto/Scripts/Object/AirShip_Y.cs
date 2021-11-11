using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Y : ObjectStateManagement_Y
{
    private Rigidbody rb;
    public float flyingHeight;
    public float rotSpeed;
    private float rotTimer;
    public Vector3 rotCenter;
    public float radius;
    private delegate IEnumerator action();
    private action[] BreakActions;
    [SerializeField] private GameObject food, chef, pollice, tank, littleChicken;

    protected override void Start()
    {
        base.Start();
        BreakActions = new action[6] { GenerateFood, GenerateChef, GeneratePollice, GenerateTank, GenerateEnemys, GenerateMiniChickens };
        rotCenter.y = flyingHeight;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (livingFlg)
        {
            rotTimer += Time.deltaTime * rotSpeed;
            transform.position = rotCenter + new Vector3(Mathf.Cos(rotTimer), 0, Mathf.Sin(rotTimer)) * radius;
            transform.LookAt(rotCenter);
            transform.forward = new Vector3(transform.forward.z, 0f, -transform.forward.x);
            rb.velocity = Vector3.zero;
        }
    }

    protected override void Death()
    {
        if (!ChangeToDeath()) return;
        tag = "Broken";
        gameObject.layer = LayerMask.NameToLayer("BrokenObject");

        //物生成
        int rnd = Random.Range(0, 100);
        int num = 0;
        if (rnd >= 0) num = 5;
        else if (rnd >= 80) num = 4;
        else if (rnd >= 70) num = 3;
        else if (rnd >= 60) num = 2;
        else if (rnd >= 50) num = 1;
        else num = 0;
        StartCoroutine(BreakActions[num]());
        DeathCount();

        //トルク付加
        rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.useGravity = true;
        var t = new float[3];
        for (int i = 0; i < 3; i++) t[i] = Random.Range(-torque, torque);
        rb.AddTorque(t[0], t[1], t[2]);

        if (breakEffect != null)
        {
            var effect = Instantiate(breakEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        rb.velocity.Set(0f, 0f, 0f);
        Delete();
    }

    private IEnumerator GenerateFood()
    {
        Vector3 blur = Vector3.zero;
        for (int i = 0; i < 100; i++)
        {
            blur.Set(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f);
            Instantiate(food, transform.position + blur, Quaternion.identity);
            yield return null;
        }
    }

    private IEnumerator GenerateChef()
    {
        Vector3 blur = Vector3.zero;
        for (int i = 0; i < 50; i++)
        {
            blur.Set(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f);
            Instantiate(chef, transform.position + blur, Quaternion.identity);
            yield return null;
        }
    }

    private IEnumerator GeneratePollice()
    {
        Vector3 blur = Vector3.zero;
        for (int i = 0; i < 50; i++)
        {
            blur.Set(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f);
            Instantiate(pollice, transform.position + blur, Quaternion.identity);
            yield return null;
        }
    }

    private IEnumerator GenerateTank()
    {
        Vector3 blur = Vector3.zero;
        for (int i = 0; i < 30; i++)
        {
            blur.Set(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f);
            Instantiate(tank, transform.position + blur, Quaternion.identity);
            yield return null;
        }
    }

    private IEnumerator GenerateEnemys()
    {
        Vector3 blur = Vector3.zero;
        for (int i = 0; i < 20; i++)
        {
            blur.Set(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f);
            Instantiate(chef, transform.position + blur, Quaternion.identity);
            yield return null;
        }
        for (int i = 0; i < 15; i++)
        {
            blur.Set(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f);
            Instantiate(pollice, transform.position + blur, Quaternion.identity);
            yield return null;
        }
        for (int i = 0; i < 15; i++)
        {
            blur.Set(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f);
            Instantiate(tank, transform.position + blur, Quaternion.identity);
            yield return null;
        }
    }

    private IEnumerator GenerateMiniChickens()
    {
        Vector3 blur = Vector3.zero;
        for (int i = 0; i < 10; i++)
        {
            blur.Set(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f);
            Instantiate(littleChicken, transform.position + blur, Quaternion.identity);
            yield return null;
        }
    }
}
