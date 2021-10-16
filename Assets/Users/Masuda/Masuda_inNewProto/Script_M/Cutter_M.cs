using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter_M : MonoBehaviour
{
    public GameObject cutterPrefab;
    public AudioClip cutterSound;
    private float timer = 0.0f;
    //次のカッターが打てるまでの間隔が3.5秒
    private float timeBetweenShot = 3.5f;
    private float power = 1000.0f;
    private float modoru;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        // もしもJキーを押したならば（条件）
        if (Input.GetKeyDown(KeyCode.J) && timer > timeBetweenShot)
        {
            timer = 0.0f;
            GameObject Cutter = Instantiate(cutterPrefab, transform.position, Quaternion.Euler(0, 0, 45));
            Rigidbody CutterRb = Cutter.GetComponent<Rigidbody>();
            //向いてる方にカッターを飛ばす
            CutterRb.AddForce(transform.forward * power);

            // 発射したカッターを3.4秒後に破壊する。
            Destroy(Cutter, 3.4f);

            //カッター射出時の音
            AudioSource sound1 = GetComponent<AudioSource>();
            sound1.PlayOneShot(cutterSound);
        }
    }
}
