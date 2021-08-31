using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3BossBuilding : ObjectStateManagement_Y
{
    private bool[] phase = new bool[3] { false, false, false };
    public float launchPower;
    public Vector3[] launchPos;
    public GameObject[] enemyData;

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

        StartCoroutine(LaunchEnemys(phaseNum));
    }

    private IEnumerator LaunchEnemys(int phaseNum)
    {
        //負荷軽減のためにコルーチンで疑似非同期化
        foreach (Transform enemy in enemyData[phaseNum].transform)
        {
            GameObject e = Instantiate(enemy.gameObject, launchPos[phaseNum], Quaternion.identity);
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
