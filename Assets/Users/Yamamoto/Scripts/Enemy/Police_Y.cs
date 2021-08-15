using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police_Y : Enemy_Y
{
    public GameObject bulletPrefab;

    protected override void Attack()
    {
        base.Attack();

        Fire();

        StartCoroutine(RestartMove());
    }

    private void Fire()
    {
        var genPos = weapon.transform.position;
        GameObject bullet = Instantiate(bulletPrefab, genPos, transform.rotation);
        bullet.transform.Rotate(90f, 0f, 0f);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
        bullet.GetComponent<BulletDamage>().damage = attackDamage;
        Destroy(bullet, 5f);
    }
}
