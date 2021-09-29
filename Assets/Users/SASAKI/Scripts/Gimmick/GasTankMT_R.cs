using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTankMT_R : MonoBehaviour
{
    [SerializeField] private bool onTheBuilding;
    [SerializeField] private GameObject enemyBill;
    private List<GameObject> buildings;

    public int CollisionCount { get { return buildings.Count; } }

    private float timer;

    //山本追加 爆発範囲
    public GameObject explosionSphere;
    public float startExplodeTiming;
    public float expDeleteTiming;
    public float expScale;
    [SerializeField] private ObjectStateManagement_Y objScr;
    private bool exploded;

    void Start()
    {
        timer = 0;
        buildings = new List<GameObject>();
    }

    void Update()
    {
        //ヤマモト追加
        if (objScr.HP <= 0 && !exploded) Explosion();

        if (!onTheBuilding)
        {
            //if (transform.parent.GetComponent<ObjectStateManagement_Y>().HP == 0)
            //Destroy(transform.parent.gameObject);

            if (CollisionCount >= 5)
            {
                objScr.GetComponent<ObjectStateManagement_Y>().HP = 0;
            }

            if (!transform.parent.GetComponent<Rigidbody>().isKinematic)
            {
                timer += Time.deltaTime;
                if (timer >= 2.0 && transform.parent.GetComponent<Rigidbody>().velocity.magnitude < 2.0)
                {
                    objScr.GetComponent<ObjectStateManagement_Y>().HP = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 15)
            return;

        foreach (var obj in buildings)
        {
            if (other == obj)
            {
                return;
            }
        }
        buildings.Add(other.gameObject);


        if (onTheBuilding && other.gameObject == enemyBill)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    //山本追加
    private void Explosion()
    {
        exploded = true;

        var sphere = Instantiate(explosionSphere, transform.position, transform.rotation);
        var expScr = sphere.GetComponent<ExplosionSphere_Y>();
        expScr.targetScale = expScale;
        expScr.deleteTime = expDeleteTiming;
        expScr.SetScalingFlg(startExplodeTiming);
    }
}
