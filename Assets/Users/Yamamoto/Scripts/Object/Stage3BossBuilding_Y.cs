using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stage3BossBuilding_Y : ObjectStateManagement_Y
{
    private bool[] phase = new bool[3] { false, false, false };
    public float launchPower;
    public Vector3[] towerPos;
    public float[] launchHeight;
    public GameObject[] enemyPrefabs;
    public int[] phaseEnemyNum;
    private GameObject mainCamera;
    private TpsCameraJC_R cameraScr;
    private CharaMoveRigid_R playerMoveScr;
    private chickenKick_R kickScr;
    private Cutter_R cutterScr;
    private MorBlast_R morblaScr;
    private EnemyMoveController_Y enemyControllerScr;
    public Vector3 cameraMoveTargetPos;
    public int[] phaseBreakEnemyNum;
    public int enemyBreakCount;
    public bool debug;
    private CriAtomSource Sound;
    private ADX_BGMAISAC aisacScr;

    protected override void Start()
    {
        base.Start();

        mainCamera = GameObject.Find("Main Camera");
        cameraScr = mainCamera.GetComponent<TpsCameraJC_R>();
        playerMoveScr = player.GetComponent<CharaMoveRigid_R>();
        kickScr = player.GetComponent<chickenKick_R>();
        cutterScr = player.GetComponent<Cutter_R>();
        morblaScr = player.GetComponent<MorBlast_R>();
        enemyControllerScr = GameObject.Find("GameAI_Y").GetComponent<EnemyMoveController_Y>();
        Sound = GetComponent<CriAtomSource>();
        aisacScr = GameObject.Find("BGMObject").GetComponent<ADX_BGMAISAC>();
    }

    private void Update()
    {
        if (debug && Input.GetKeyDown(KeyCode.Return))
        {
            changeDamageFlg();
            Damage(kickMag, 1);
        }
    }

    public override void Damage(float mag, int skill)
    {
        //すでに破壊済みの場合は何も起きないようにする
        if (!livingFlg) return;
        notDamage = true;
        Invoke("changeDamageFlg", 0.5f);

        HP -= (int)(scrEvo.Status_ATK * mag);

        LaunchCheck(HPCheck(HP));

        SetSkillID(skill);
        //生死判定
        LivingCheck();
    }

    private int HPCheck(int HP)
    {
        //HPがLaunchタイミングになっているかを調べる。
        if (HP <= MaxHP / 4) return 2;
        else if (HP <= MaxHP / 4 * 2) return 1;
        else if (HP <= MaxHP / 4 * 3) return 0;

        //どのLaunchタイミングでもない(HP > MaxHP / 4 * 3)だった場合は3を返す
        return -1;
    }

    private void LaunchCheck(int phaseNum)
    {
        //Launchタイミングでない場合は無視
        if (phaseNum == -1) return;
        //すでにそのphaseでLaunch済みであれば無視
        else if (phase[phaseNum]) return;

        Debug.Log($"Phase : {phaseNum + 1}");
        phase[phaseNum] = true;
        ChangeToCameraMode();

        /*
            もし敵の射出に関して処理を追加したい場合はここに作成した関数を記述してください
        */
        Sound.Play("Warning");
        if(aisacScr.St3Fase == false)aisacScr.St3Fase = true;

        StartCoroutine(LookLauncher(phaseNum));
        StartCoroutine(LaunchEnemys(phaseNum));
    }

    private IEnumerator LookLauncher(int phaseNum)
    {
        var nowPos = mainCamera.transform.position;
        var lookPos = new Vector3(transform.position.x, launchHeight[phaseNum], transform.position.z);
        for (float i = 0; i < 1.0f; i += 0.005f)
        {
            mainCamera.transform.position = Vector3.Lerp(nowPos, cameraMoveTargetPos, i);
            mainCamera.transform.LookAt(lookPos);
            yield return null;
        }
        Invoke("ChangeToPlayMode", 2f);
    }

    private IEnumerator LaunchEnemys(int phaseNum)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < phaseEnemyNum[i + phaseNum * 3]; j++)
            {
                int num = Random.Range(0, 2);
                var genPos = new Vector3(towerPos[num].x, launchHeight[i], towerPos[num].z);
                GameObject e = Instantiate(enemyPrefabs[i].gameObject, genPos, Quaternion.identity);
                e.GetComponent<FlyingEnemy_Y>().SetSt3BossScr(this);
                var rb = e.GetComponent<Rigidbody>();
                var dir = Vector3.zero;

                //射出方向の抽選
                while (true)
                {
                    dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                    if (dir.magnitude >= 1f) break;
                }

                e.transform.eulerAngles = dir;
                dir.y = 2f;
                rb.AddForce(dir * launchPower, ForceMode.Impulse);

                yield return null;
            }
        }
        notDamage = true;
    }

    private void ChangeToCameraMode()
    {
        cameraScr.enabled = false;
        playerMoveScr.enabled = false;
        kickScr.enabled = false;
        cutterScr.enabled = false;
        morblaScr.enabled = false;
        enemyControllerScr.enemyCanMove = false;
    }

    private void ChangeToPlayMode()
    {
        cameraScr.enabled = true;
        playerMoveScr.enabled = true;
        kickScr.enabled = true;
        cutterScr.enabled = true;
        morblaScr.enabled = true;
        enemyControllerScr.enemyCanMove = true;
    }

    public void IncreaseEnemyBreakCount()
    {
        enemyBreakCount++;
        int phaseNum = 0;
        foreach (var p in phase) if (p) phaseNum++;

        if (enemyBreakCount >= phaseBreakEnemyNum[phaseNum - 1])
        {
            changeDamageFlg();
            //もし敵を指定の数倒して支部が攻撃できるようになることに何か処理を追加する場合は、この下に書いてください
        }
    }

    protected override void Death()
    {
        base.Death();
        var saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager_Y>();
        saveManager.SaveClearFlg(2);
    }
}
