using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTank_R : MonoBehaviour
{
    [SerializeField] private bool onTheBuilding;
    [SerializeField] private Rigidbody mainTankRigid;
    [SerializeField] private int speed;
    private GameObject[] props;

    private Vector3 moveVec;    // 転がる方向

    // Start is called before the first frame update
    void Start()
    {
        props = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            props[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!onTheBuilding)
        {
            // 足場が破壊されたとき
            foreach (var obj in props)
            {
                if (obj.GetComponent<ObjectStateManagement_Y>().HP == 0)
                {
                    SetPropsIsTrigger();
                    Vector3 dist = (obj.transform.position - this.transform.position);
                    moveVec = new Vector3(dist.x, 0, dist.z).normalized;
                    mainTankRigid.isKinematic = false;
                    mainTankRigid.AddForce(moveVec * speed);
                }
            }
        }
    }

    private void SetPropsIsTrigger()
    {
        foreach (var obj in props)
        {
            obj.GetComponent<MeshCollider>().isTrigger = true;
        }
    }
}
