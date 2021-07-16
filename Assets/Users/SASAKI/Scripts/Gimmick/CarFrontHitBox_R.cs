using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFrontHitBox_R : MonoBehaviour
{
    private void OnTriggerStay(Collider collider)
    {
        if (collider.transform.tag == "Player")
        {
            if(collider.gameObject.GetComponentInParent<EvolutionChicken_R>().EvolutionNum <= 2)
            {
                collider.gameObject.GetComponentInParent<Rigidbody>().AddExplosionForce(5f, transform.position, 20f, 1.5f, ForceMode.Impulse);
                collider.gameObject.GetComponentInParent<CharaMoveRigid_R>().stunFlag = true;
            }
        }
    }
}
