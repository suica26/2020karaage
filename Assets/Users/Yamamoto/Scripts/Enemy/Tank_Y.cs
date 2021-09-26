using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Y : Enemy_Y
{
    public GameObject bulletPrefab;

    private void Fire()
    {
        var genPos = weapon.transform.position;
        GameObject bullet = Instantiate(bulletPrefab, genPos, transform.rotation);
        bullet.transform.Rotate(90f, 0f, 0f);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 50f;
        bullet.GetComponent<BulletDamage>().damage = attackDamage;
        Destroy(bullet, 5f);
    }

    public override void Damage(float mag, int skill)
    {
        //すでに破壊済みの場合は何も起きないようにする
        if (!livingFlg) return;

        HP -= (int)(scrEvo.Status_ATK * mag);
        SetSkillID(skill);
        //生死判定
        LivingCheck();
    }

    protected override void Death()
    {
        //すでに破壊済みの場合は無視
        if (!ChangeToDeath()) return;

        //アニメーション以外の要素を停止
        StopMove();
        animator.enabled = false;
        navAgent.enabled = false;

        Destroy(gameObject, deleteTime);

        if (divideObject != null && hitSkilID != 2) ChangeObject();
        else Substitution();    //差し替えをしない場合
    }
}
