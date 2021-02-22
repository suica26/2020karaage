using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusForGameAI : MonoBehaviour
{
    private EnemySpawnController AIscript;
    public int i;

    // Start is called before the first frame update
    void Start()
    {
        AIscript = GameObject.Find("GameAI_Y").GetComponent<EnemySpawnController>();
    }

    private void OnDestroy()
    {
        AIscript.UpdateEnemyNum(i);
    }
}
