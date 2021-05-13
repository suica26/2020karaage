using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civil_Y : MonoBehaviour
{
    //市民を実装する際には、必ずTagでCivilを設定すること
    public bool escapeFlg = false;
    public float escapeSpeed;
    public float contagionRange;
    private Vector3 fallenSpeed;
    private Rigidbody rb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //y方向の速度を保存
        fallenSpeed = new Vector3(0, rb.velocity.y, 0);
        //自分の正面方向に進むように設定
        rb.velocity = transform.forward * escapeSpeed + fallenSpeed;

        if (escapeFlg)
        {
            Escape();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit!");
        if (!escapeFlg)
        {
            //ダメージを受けると逃げるフラグがたつ
            if (other.gameObject.name == "KickCollision" ||
                other.gameObject.name == "MorningBlastSphere_Y(Clone)" ||
                other.gameObject.name == "Cutter(Clone)" ||
                other.gameObject.tag == "Chain" ||
                other.gameObject.name == "fallAttackCircle(Clone)")
            {
                Debug.Log("Damage!");
                EscapeContagion();
            }
        }
    }

    private void EscapeContagion()
    {
        Escape();
        var civilObjects = GameObject.FindGameObjectsWithTag("Civil");
        foreach (var civils in civilObjects)
        {
            if (GetVectorXZ(transform.position, civils.transform.position).magnitude < contagionRange)
            {
                civils.GetComponent<Civil_Y>().Escape();
            }
        }
    }

    //XZ平面における点Aから点Bへの方向ベクトルを返す。(yは0)
    private Vector3 GetVectorXZ(Vector3 A, Vector3 B)
    {
        var AB = A - B;
        AB.y = 0;
        return AB;
    }

    public void Escape()
    {
        Debug.Log("Escape!");
        if (!escapeFlg) escapeFlg = true;
        transform.forward = GetVectorXZ(transform.position, player.transform.position).normalized;
    }
}
