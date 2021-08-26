using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTank_R : MonoBehaviour
{
    [SerializeField] private bool onTheBuilding;
    [SerializeField] private Rigidbody mainTankRigid;
    [SerializeField] private int speed;
    [SerializeField] private GameObject subprops;
    [SerializeField] private ObjectStateManagement_Y underObject;
    private GameObject[] props;

    private Vector3 moveVec;    // 転がる方向
    private bool isDelete;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        isDelete = false;
        timer = 0;
        props = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            props[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainTankRigid == null)
            isDelete = true;

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
        else
        {
            if(underObject.HP == 0 && !isDelete)
            {
                SetPropsIsTrigger();
                mainTankRigid.isKinematic = false;
                moveVec = new Vector3(20.0f, 0.0f, 0.0f);
                mainTankRigid.AddForce(moveVec, ForceMode.Impulse);
            }
        }

        // 暫定処理
        if(isDelete)
        {
            timer += Time.deltaTime;
            if (timer >= 2.0)
            {
                Destroy(this.gameObject);
                Destroy(this.subprops);
            }
        }
    }

    // 他の足場も削除する
    private void SetPropsIsTrigger()
    {
        isDelete = true;
        foreach (var obj in props)
        {
            obj.GetComponent<MeshCollider>().isTrigger = true;
        }
    }
}
