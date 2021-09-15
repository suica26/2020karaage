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
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 50f;
        bullet.GetComponent<BulletDamage>().damage = attackDamage;
        Destroy(bullet, 5f);
    }
}
