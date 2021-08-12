using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attention_Field_M : MonoBehaviour
{
    [SerializeField] GameObject[] walls;
    [SerializeField] GameObject player, attention;
    [SerializeField] public float safety, count;
    [SerializeField] Animator animator;
    private string str1 = "isSwitch";
    private string str2 = "isTap";
    [SerializeField] private bool bar;

    void Start()
    {
        //this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject wall in walls)
        {
            if (Vector3.Distance(player.transform.position, wall.transform.position) <= safety)
            {
                animator.SetBool(str1, true);
                bar = true;
            }

            if (Vector3.Distance(player.transform.position, wall.transform.position) > safety && bar)
            {
                animator.SetBool(str2, true);
                Count();
            }
        }

        if (count >= 1.1f)
        {
            count = 0;
            bar = false;
            animator.SetBool(str1, false);
            animator.SetBool(str2, false);
        }
    }

    private void Count()
    {
        count += Time.deltaTime;
    }
}
