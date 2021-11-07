using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBack_M : MonoBehaviour
{
    [SerializeField] GameObject objParam;
    [SerializeField] private Image image1, image2;
    private int linePoint, point;
    private Parameters_R scr;
    void Start()
    {
        scr = objParam.gameObject.GetComponent<Parameters_R>();
        image1.enabled = true;
        image2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        point = ScoreAttack_Y.score;
        if (point == 1000000)
        {
            image1.enabled = false;
            image2.enabled = true;
        }
    }
}
