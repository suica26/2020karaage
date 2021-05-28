using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickCollpsion : MonoBehaviour
{
    public GameObject player;
    public float power;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        //キックダメージ
        if (other.gameObject.name == "KickCollision")
        {
            KickCollapse(this.gameObject, player.transform.position);
        }
    }
    */
    private void RigidOn(GameObject obj)
    {
        var rb = obj.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    private void KickCollapse(GameObject obj, Vector3 P)
    {

        Debug.Log("Kick!");
        RigidOn(obj);

        float Torque = 100f;
        var direction = obj.transform.position - P;
        var rb = obj.GetComponent<Rigidbody>();

        direction = direction.normalized;
        var F = direction * power;
        Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
    }
}
