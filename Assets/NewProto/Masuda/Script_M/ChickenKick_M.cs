using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenKick_M : MonoBehaviour
{
    public GameObject kickEffect;
    public AudioClip kickSound;

    void Update()
    {
        Vector3 p = new Vector3(0, 0, 2);
        Vector3 q = new Vector3(0, 0, -2);
        // もしもKキーを押したならば（条件）
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.Translate(p);

            //サウンド再生
            AudioSource sound_kanri = GetComponent<AudioSource>();
            sound_kanri.PlayOneShot(kickSound);

            //エフェクト
            Instantiate(kickEffect, transform.position, Quaternion.identity);　//エフェクト発生

        }
        //キーが上がったら足が戻る
        if (Input.GetKeyUp(KeyCode.K))
        {
            transform.Translate(q);
        }
    }
}
