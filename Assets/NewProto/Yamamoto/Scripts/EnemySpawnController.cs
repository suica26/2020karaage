using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField, Header("敵Prefab")] private GameObject[] enemyPrefab;
    [SerializeField, Header("湧き場所")] private GameObject[] spawner;
    [SerializeField, Header("最大湧き数")] private int[] maxNum;
    [SerializeField, Header("リスポーン時間")] private float[] respawnTime;
    [SerializeField, Header("どのPhaseで出現するか")] private int[] spawnPhase;
    [Header("スポーン位置のブレの量")]public float blur = 5f;

    private GameObject player;
    private EvolutionChicken_R scrEvo;
    private float[] respawnTimer;
    private List<List<Transform>> spawnPos;
    private int[] enemyNum;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scrEvo = player.GetComponent<EvolutionChicken_R>();
        respawnTimer = new float[enemyPrefab.Length];
        enemyNum = new int[enemyPrefab.Length];
        spawnPos = new List<List<Transform>>();

        
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            //配列初期化
            respawnTimer[i] = 0f;
            enemyNum[i] = 0;
            foreach(Transform pos in spawner[i].transform)
            {
                //spawn位置のインポート
                spawnPos.Add(new List<Transform>());
                spawnPos[i].Add(pos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i= 0; i < enemyPrefab.Length; i++)
        {
            if (scrEvo.EvolutionNum + 1 >= spawnPhase[i] && enemyNum[i] < maxNum[i])
            {
                respawnTimer[i] += Time.deltaTime;
                if (respawnTimer[i] >= respawnTime[i])
                {
                    Spawn(i);
                }
            }
        }
    }

    private void Spawn(int i)
    {
        respawnTimer[i] = 0f;
        var spawnP = randomSpawnPos(spawnPos[i]);
        var enemy = Instantiate(enemyPrefab[i], SpawnPositionBlur(spawnP), Quaternion.identity);
        enemyNum[i]++;
        var scr = enemy.AddComponent<EnemyStatusForGameAI>();
        scr.i = i;  //何番目の敵(Prefab)なのかを記憶させる
        //Debug.Log($"{i}番目の敵は,{enemyNum[i]}体います");
    }

    //スポーン候補地点からランダムにスポーン位置を決定
    private Transform randomSpawnPos(List<Transform> spawnPosList)
    {
        return spawnPosList[Random.Range(0, spawnPosList.Count)];
    }

    private Vector3 SpawnPositionBlur(Transform spawnPos)
    {
        var pos = spawnPos.position;
        pos.x += Random.Range(-blur, blur);
        pos.z += Random.Range(-blur, blur);
        return pos;
    }

    public void UpdateEnemyNum(int i)
    {
        enemyNum[i]--;
    }
}
