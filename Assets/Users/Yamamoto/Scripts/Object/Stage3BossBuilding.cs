using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stage3BossBuilding : ObjectStateManagement_Y
{
    private bool[] phase = new bool[3] { false, false, false };
    public float launchPower;
    public Vector3[] launchPos;
    public GameObject[] enemyPrefabs;
    public int[] phaseEnemyNum;
    private GameObject mainCamera;
    private TpsCameraJC_R cameraScr;
    private CharaMoveRigid_R playerMoveScr;
    private EnemyMoveController_Y enemyControllerScr;
    public Vector3 cameraMoveTargetPos;

    protected override void Start()
    {
        base.Start();

        mainCamera = GameObject.Find("Main Camera");
        cameraScr = mainCamera.GetComponent<TpsCameraJC_R>();
        playerMoveScr = player.GetComponent<CharaMoveRigid_R>();
        enemyControllerScr = GameObject.Find("GameAI_Y").GetComponent<EnemyMoveController_Y>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) Damage(kickMag, 1);
    }

    public override void Damage(float mag, int skill)
    {
        //すでに破壊済みの場合は何も起きないようにする
        if (!livingFlg) return;

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

        StartCoroutine(LookLauncher(phaseNum));
        StartCoroutine(LaunchEnemys(phaseNum));
    }

    private IEnumerator LookLauncher(int phaseNum)
    {
        var nowPos = mainCamera.transform.position;
        for (float i = 0; i < 1.0f; i += 0.005f)
        {
            mainCamera.transform.position = Vector3.Lerp(nowPos, cameraMoveTargetPos, i);
            mainCamera.transform.LookAt(launchPos[phaseNum]);
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
                GameObject e = Instantiate(enemyPrefabs[i].gameObject, launchPos[i], Quaternion.identity);
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
    }

    private void ChangeToCameraMode()
    {
        cameraScr.enabled = false;
        playerMoveScr.enabled = false;
        enemyControllerScr.enemyCanMove = false;
    }

    private void ChangeToPlayMode()
    {
        cameraScr.enabled = true;
        playerMoveScr.enabled = true;
        enemyControllerScr.enemyCanMove = true;
    }
}
