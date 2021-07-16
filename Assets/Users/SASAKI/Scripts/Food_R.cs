using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_R : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private int addEP;
    private Parameters_R scrEP;
    //ADX
    private new CriAtomSource audio;

    void Start()
    {
        scrEP = GameObject.Find("Canvas").GetComponent<Parameters_R>();
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audio.Play("FeedGet00");
            scrEP.EPManager(addEP);

            //山本修正
            //消去を1秒遅らせて取得音が鳴るように調整
            Destroy(gameObject, 1f);
            //消去が1秒遅れるので、非アクティブにして見えなくしとく
            gameObject.SetActive(false);
        }
    }
}
