using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef_Y : Enemy_Y
{
    public GameObject hitBoxPrefab;
    private GameObject hitBox;

    private void CreateHitBox()
    {
        var genPos = transform.position + transform.forward * 2f;
        hitBox = Instantiate(hitBoxPrefab, genPos, Quaternion.identity);
        hitBox.GetComponent<Collider>().isTrigger = true;
        hitBox.GetComponent<HitBoxDamage_Y>().damage = attackDamage;
    }

    private void DeleteHitBox()
    {
        Destroy(hitBox);
    }

    private void OnDestroy()
    {
        if (hitBox != null) Destroy(hitBox);
    }
}
