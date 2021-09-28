using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStation_Y : ObjectStateManagement_Y
{
    public GameObject explosionSphere;
    public float startExplodeTiming;
    public float expDeleteTiming;
    public float expScale;
    protected override void Death()
    {
        if (!ChangeToDeath()) return;

        DeathCount();
        Destroy(gameObject, deleteTime);
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
        var sphere = Instantiate(explosionSphere, genPos, Quaternion.identity);
        var expScr = sphere.GetComponent<ExplosionSphere_Y>();
        expScr.targetScale = expScale;
        expScr.deleteTime = expDeleteTiming;
        expScr.SetScalingFlg(startExplodeTiming);

        yield return new WaitForSeconds(startExplodeTiming);

        gameObject.SetActive(false);

        //差し替え処理
        var dividedObject = Instantiate(divideObject, transform.position, transform.rotation);
        Destroy(dividedObject, deleteTime);

        //破壊オブジェクト設定
        tag = "Broken";
        gameObject.layer = LayerMask.NameToLayer("BrokenObject");

        //破壊処理
        foreach (Transform child in dividedObject.transform)
        {
            GasStationBreak(child.gameObject);
        }
        if (breakEffect != null)
        {
            var effect = Instantiate(breakEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
        }
    }
}