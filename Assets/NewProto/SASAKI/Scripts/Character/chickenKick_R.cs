using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenKick_R : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject[] kickCollisions;
    [SerializeField] GameObject kickEffect;
    [SerializeField] AudioClip kickSound;
    [SerializeField] Transition_R[] scrAnim;

    EvolutionChicken_R scrEvo;

    public int chargePoint;
    private float timer;

    //ADX
    public new CriAtomSource audio;

    // Start is called before the first frame update
    void Start()
    {
        scrEvo = GetComponent<EvolutionChicken_R>();
        timer = 0.0f;
        chargePoint = 0;
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(timer <= 0.5f)
            {
                timer = 0.0f;
                //audioSource.PlayOneShot(kickSound);
                audio.Play("Kick");
                var objKick = Instantiate(kickEffect, transform.position, Quaternion.identity);
                Destroy(objKick, 0.5f);
                kickCollisions[scrEvo.EvolutionNum].SetActive(true);
                scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.KICK, true);
            }
            else
            {
                timer = 0.0f;
            }
        }
        else
        {
            kickCollisions[scrEvo.EvolutionNum].SetActive(false);
            scrAnim[scrEvo.EvolutionNum].SetAnimator(Transition_R.Anim.KICK, false);
        }
    }
}
