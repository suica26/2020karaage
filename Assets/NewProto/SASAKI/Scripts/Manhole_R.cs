using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manhole_R : MonoBehaviour
{
    [SerializeField] private GameObject objBlow;
    [Tooltip("使用するギミックをPrefab上で設定してください"), Range(0, 2), SerializeField] private int gimmickNum;
    [Header("共通設定")]
    [Tooltip("ギミックの利用可能段階を設定する"),Range(0, 3), SerializeField] private int usageEvo;
    [Tooltip("ForceModeを切り替える(False -> ForceMode.Force, True -> ForceMode.Impulse"), SerializeField] private bool forceMode_Impulse;
    [SerializeField] private float force;
    [SerializeField] private float time;
    [Tooltip("噴水のスケールを設定"),SerializeField] private int x, y, z;
    [SerializeField] private float hosei;

    [Header("消火栓設定(GimmickNum = 1)")]
    [SerializeField] private ObjectStateManagement_Y scrObj;
    private bool makeHydrant = false;

    [Header("換気口設定(GimmickNum = 2)")]
    [SerializeField] private int MinIntervalTime;
    [SerializeField] private int MaxIntervalTime;
    [SerializeField] private GameObject windEffect;
    private float intervalTime;

    private GameObject blow;

    private void Start()
    {
        if (gimmickNum == 2) intervalTime = Random.Range(MinIntervalTime, MaxIntervalTime);
    }

    private void Update()
    {
        if(gimmickNum == 1 && !makeHydrant)
        {
            if(scrObj.HP <= 0)
            {
                makeHydrant = true;
                InstanceBlow();
            }
        }
        if(gimmickNum == 2)
        {
            intervalTime -= Time.deltaTime;
            if(intervalTime <= 0f)
            {
                intervalTime = Random.Range(MinIntervalTime, MaxIntervalTime);
                if (windEffect != null)
                    Instantiate(windEffect, transform.position - Vector3.up * 0.7f + (Vector3.up * y / 2), Quaternion.identity); ;
                InstanceBlow();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //マンホールのギミック        
        if (gimmickNum == 0 && other.gameObject.name == "fallAttackCircle(Clone)")       
        {
            InstanceBlow();        
        }
    }

    private void InstanceBlow()
    {
        blow = Instantiate(objBlow, transform.position + (Vector3.up * (y / 2 - hosei)), Quaternion.identity);
        blow.transform.localScale = new Vector3(x, y, z);
        blow.GetComponent<BlowWind_R>().force = force;
        blow.GetComponent<BlowWind_R>().impulse = forceMode_Impulse;
        blow.GetComponent<BlowWind_R>().usageEvo = usageEvo;
        Destroy(blow, time);
    }
}
