using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBump_Y : MonoBehaviour
{
    private Animator animator;
    public float durationTime;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > durationTime)
        {
            timer = 0f;
            animator.SetTrigger("Bump");
        }
    }
}
