using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAttack_Y : MonoBehaviour
{
    public static ScoreAttack_Y instance { get; private set; }
    public float limitTime { get; private set; }
    public int score { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        return;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
    }

    public void Init()
    {
        limitTime = 3f;
        score = 0;
    }
}
