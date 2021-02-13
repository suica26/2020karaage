using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMove : MonoBehaviour
{
    private EnemyNav_Y navScript;
    private float routineTimer = 0f;
    //攻撃をした際の次の行動までの時間
    public float fireFreeze = 3f;    //発砲時
    public float hitFleeze = 1f;    //警棒で殴った時
    public GameObject bulletPrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        navScript = this.gameObject.GetComponent<EnemyNav_Y>();
    }

    // Update is called once per frame
    void Update()
    {
        if (routineTimer > 0f) routineTimer -= Time.deltaTime;

        if(navScript.navFlg)
        {
            if(routineTimer <= 0f)
            {
                int value = Random.Range(0, 1 + 1);
                if (value == 0) Fire();
                else if (value == 1) Hit();
            }
        }
    }

    private void Fire()
    {
        routineTimer = fireFreeze;
        GameObject bullet = Instantiate(bulletPrefab);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
    }

    private void Hit()
    {
        routineTimer = hitFleeze;
    }
}
