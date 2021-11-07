using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAttackLight_Y : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScoreAttack_Y.directionalLight = gameObject;
    }
}
