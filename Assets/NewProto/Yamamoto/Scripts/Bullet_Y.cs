using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Y : MonoBehaviour
{
    //このScriptは弾丸につけます

    public AnimationClip reflect;
    [SerializeField] private GameObject player = null;
    Animator bulletAnimator;
    Rigidbody thisRB;
    [Range(10f, 100f)] public float afterReflectSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        bulletAnimator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReflectBullet(GameObject morBla)
    {
        StartCoroutine(Reflection(morBla));
    }

    IEnumerator Reflection(GameObject morBla)
    {
        player = GameObject.Find("Player");
        var forward = (player.transform.position - this.gameObject.transform.position).normalized;
        this.gameObject.transform.forward = forward;
        thisRB = this.gameObject.GetComponent<Rigidbody>();
        bulletAnimator.SetTrigger("isReflect");
        thisRB.velocity = Vector3.zero;

        yield return new WaitForSeconds(reflect.length);

        thisRB.velocity = -forward * afterReflectSpeed;
    }
}
