using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTime : MonoBehaviour
{
    private float evoTimer, termTime = 45;
    private Parameters_R param;

    void Update()
    {
        evoTimer += Time.deltaTime;

        if (evoTimer >= termTime)
        {
            evoTimer = 0;
        }
    }
}
