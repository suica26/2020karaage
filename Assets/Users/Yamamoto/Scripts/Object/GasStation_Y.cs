using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStation_Y : ObjectStateManagement_Y
{
    public float createExplodeTiming;
    public GameObject explosionSphere;
    protected override void Death()
    {
        if (!ChangeToDeath()) return;

        if (GetComponent<Car_R>() != null)
        {
            Destroy(GetComponent<Animator>());
            Destroy(GetComponent<Car_R>());
        }

        DeathCount();
        Destroy(gameObject, deleteTime);
        gameObject.SetActive(false);

        //差し替え処理
        var dividedObject = Instantiate(divideObject, transform.position, transform.rotation);

        //破壊オブジェクト設定
        tag = "Broken";
        gameObject.layer = LayerMask.NameToLayer("BrokenObject");

        //破壊処理
        foreach (Transform child in gameObject.transform)
        {
            GasStationBreak(child.gameObject);
            Destroy(child.gameObject, deleteTime);
        }
        if (breakEffect != null)
        {
            var effect = Instantiate(breakEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
        }
        StartCoroutine(InstantiateExplode(transform.position));
    }

    private void RigidOn(GameObject obj)
    {
        var rb = obj.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.useGravity = true;
    }

    private void GasStationBreak(GameObject obj)
    {
        RigidOn(obj);

        var dir = (obj.transform.position - transform.position).normalized;
        var F = dir * power * 1.5f;
        var rb = GetComponent<Rigidbody>();
        Vector3 TorquePower = new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque));
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
    }

    private IEnumerator InstantiateExplode(Vector3 genPos)
    {
        yield return new WaitForSeconds(createExplodeTiming);
        Instantiate(explosionSphere, genPos, Quaternion.identity);
    }
}