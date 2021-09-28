using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boxhp : MonoBehaviour
{
    public int damage;
    public int maxHp = 155;
    private int currentHp;
    public Slider slider;
    public GameObject uipanel;
    //ADX
    public new CriAtomSource audio;

    private Animator animCon;
    private bool death;
    public commentplaykick scrCommentKick;

    // Start is called before the first frame update
    void Start()
    {
        animCon = GetComponent<Animator>();

        audio = GetComponent<CriAtomSource>();

        slider.value = 1;
        currentHp = maxHp;
        death = false;
        uipanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)currentHp / (float)maxHp;

        //破壊したとき
        if (currentHp <= 0 && !death)
        {
            audio.Play("BoxBreak");
            death = true;
            currentHp = 0;
            scrCommentKick.BreakNumPlus();
            scrCommentKick.animCon.SetInteger("play", 0);
            animCon.SetInteger("break", 1);
            Destroy(this.gameObject, 1.2f);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "KickCollision")
        {
            currentHp -= damage;
            uipanel.SetActive(true);
            Invoke("UISetNonActive", 1.2f);
            if (audio.status != CriAtomSource.Status.Playing)
                audio.Play("BoxContract");
        }
    }

    private void UISetNonActive()
    {
        uipanel.SetActive(false);
    }
}
