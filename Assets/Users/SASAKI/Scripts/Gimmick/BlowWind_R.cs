using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowWind_R : MonoBehaviour
{
    public float force;
    public int usageEvo;
    public bool impulse;
    Rigidbody rigid;
    GameObject player;
    EvolutionChicken_R scrEvo;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rigid = player.GetComponent<Rigidbody>();
        scrEvo = player.GetComponent<EvolutionChicken_R>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && usageEvo >= scrEvo.EvolutionNum)
        {
            if (impulse)
                rigid.AddForce(Vector3.up * force, ForceMode.Impulse);
            else
                rigid.AddForce(Vector3.up * force);

            player.GetComponent<CharaMoveRigid_R>()._isFlying = true;
        }
    }
}
