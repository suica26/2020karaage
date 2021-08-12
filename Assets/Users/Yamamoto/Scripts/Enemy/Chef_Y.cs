using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef_Y : Enemy_Y
{
    public GameObject hitBoxPrefab;

    protected override void Attack()
    {
        base.Attack();

        var hitBox = CreateDamageBox();

        StartCoroutine(RestartMove());
        DeleteHitBox(hitBox);
    }

    private GameObject CreateDamageBox()
    {
        var genPos = transform.position + transform.forward * 2f;
        genPos.y = 0.5f;
        var hitBox = Instantiate(hitBoxPrefab, genPos, Quaternion.identity, transform.Find("Body"));
        hitBox.GetComponent<Collider>().isTrigger = true;
        hitBox.GetComponent<HitBoxDamage_Y>().damage = attackDamage;
        return hitBox;
    }

    private void DeleteHitBox(GameObject _hitBox)
    {
        Destroy(_hitBox);
    }
}
