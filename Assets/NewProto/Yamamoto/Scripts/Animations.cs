using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public float power = 1000f;

    //ニワトリ君につけてください
    public void KnockBack(GameObject gameObject)
    {
        var A = this.gameObject.transform.position;
        var B = gameObject.transform.position;
        var dir = A - B;
        dir.y = 0f;
        dir = dir.normalized;
        dir.y = 2f;
        var F = dir * power;
        this.gameObject.GetComponent<Rigidbody>().AddForce(F, ForceMode.Impulse);
    }
}
