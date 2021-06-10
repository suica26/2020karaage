using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseAnimator_M : MonoBehaviour
{
    [SerializeField] private GameObject player,shop,mark;
    [SerializeField] Animator mouse;
    void Start()
    {
        player = GameObject.Find("Player");
        mark = GameObject.Find("Mouse");
        mouse.Play("mouse");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 shopPos = shop.transform.position;
        float dist = Vector3.Distance(playerPos, shopPos);
        if (dist <= 8)
        {
            Destroy(mark);
        }
    }
}
