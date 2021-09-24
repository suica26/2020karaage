using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusForGameAI : MonoBehaviour
{
    public EnemySpawnController AIscript;
    public int i;

    private void OnDestroy()
    {
        AIscript?.UpdateEnemyNum(i);
    }
}
