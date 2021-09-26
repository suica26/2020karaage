using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStation_Y : ObjectStateManagement_Y
{
    protected override void Death()
    {
        if (!ChangeToDeath()) return;

        if (GetComponent<Car_R>() != null)
        {
            Destroy(GetComponent<Animator>());
            Destroy(GetComponent<Car_R>());
        }

        DeathCount();
        Destroy(gameObject, deleteTime);


    }
}