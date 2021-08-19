using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BLINDED_AM_ME;

public class ObjectBreak_Y : MonoBehaviour
{
    private ObjectStateManagement_Y objScr;
    private GameObject player;
    private CriAtomSource criAtomSource;

    // 自身の子要素を管理するリスト
    private List<GameObject> myParts = new List<GameObject>();

    private delegate void VoidFunc(GameObject obj);
    private VoidFunc[] Collapsions;

    /// <summary>
    /// true: 分割済み
    /// false: そのままのObject
    /// </summary>
    public bool divided;

    public void InitSetting(ObjectStateManagement_Y objectScript, bool isDivided)
    {
        objScr = objectScript;
        player = objectScript.player;
        Collapsions = new VoidFunc[6] { TramplingCollapse, KickCollapse, CutterCollapse, MorBlaBreak, KickCollapse, KickCollapse };
        criAtomSource = GetComponent<CriAtomSource>();
        //破壊済み状態にタグとレイヤーを変更
        tag = "Broken";
        this.gameObject.layer = LayerMask.NameToLayer("BrokenObject");
        //分割済みオブジェクトなのかを判定
        divided = isDivided;

        if (divided)
        {
            //自分の子要素をリストに格納
            foreach (Transform child in gameObject.transform)
            {
                var rb = child.gameObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;
                myParts.Add(child.gameObject);
                if (child.gameObject.GetComponent<Collider>() == null) child.gameObject.AddComponent<BoxCollider>();
            }
        }
    }

    public void BreakAction()
    {
        //破砕片になる場合とオブジェクトが差し替えられない場合で動作を変更
        if (divided)
        {
            foreach (var shard in myParts)
            {
                Collapsions[objScr.hitSkilID](shard);
            }
        }
        else
        {
            ShardSettings(gameObject, objScr.shardDamage_nonDiv);
            Collapsions[objScr.hitSkilID](gameObject);
        }

        if (objScr.breakEffect != null)
        {
            var effect = Instantiate(objScr.breakEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        Destroy(gameObject, objScr.deleteTime);
    }

    private void RigidOn(GameObject obj)
    {
        var rb = obj.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.useGravity = true;
    }

    //オブジェクト単体の場合の設定関数
    private void ShardSettings(GameObject obj, int nonDivDamage)
    {
        obj.layer = LayerMask.NameToLayer("Shard");

        var shardScr = obj.AddComponent<Shard_Y>();
        shardScr.shardDamage = nonDivDamage;
    }

    //踏み潰し攻撃
    private void TramplingCollapse(GameObject obj)
    {
        RigidOn(obj);
        obj.GetComponent<Rigidbody>().AddForce(Random.Range(0, objScr.power / 10), -objScr.power * 10, Random.Range(0, objScr.power / 10));
    }

    //チキンキック
    private void KickCollapse(GameObject obj)
    {
        RigidOn(obj);

        var D = (transform.position - player.transform.position).normalized;   //力の方向
        var rb = obj.GetComponent<Rigidbody>();
        var F = D * objScr.power;
        Vector3 TorquePower = new Vector3(Random.Range(-objScr.torque, objScr.torque), Random.Range(-objScr.torque, objScr.torque), Random.Range(-objScr.torque, objScr.torque));
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
    }

    //とさかカッター
    private void CutterCollapse(GameObject obj)
    {
        RigidOn(obj);
        GameObject cutter = GameObject.Find("Cutter(Clone)");
        //カッターの進行方向（XZ平面）を90度回転したものがカット平面
        var normal = cutter.transform.right;
        var leftObjects = new List<GameObject>();
        var rightObjects = new List<GameObject>();

        var left = new GameObject(gameObject.name + "_leftObj", typeof(Rigidbody));
        var right = new GameObject(gameObject.name + "_rightObj", typeof(Rigidbody));

        //MeshCutがException吐いた時のために、一応先に宣言しておく
        Destroy(left, objScr.deleteTime);
        Destroy(right, objScr.deleteTime);

        //Meshがついているか確認するリスト
        var checkObjects = new List<GameObject>();
        //最初は代入されたobjをとりあえず入れておく
        checkObjects.Add(obj);
        bool finish = false;

        while (!finish)
        {
            var nextCheck = new List<GameObject>();
            foreach (var cObj in checkObjects)
            {
                //メッシュが設定されている(＝空オブジェクトでない)オブジェクトの場合
                if (cObj.GetComponent<MeshFilter>() != null)
                {
                    var divObjects = MeshCut.Cut(cObj, cutter.transform.position, normal, cObj.GetComponent<Renderer>().material);
                    divObjects[0].transform.parent = left.transform;
                    divObjects[1].transform.parent = right.transform;
                }
                else
                {
                    //子オブジェクトを所有しているかを確認
                    if (cObj.transform.childCount > 0)
                    {
                        foreach (Transform children in cObj.transform)
                        {
                            nextCheck.Add(children.gameObject);
                        }
                    }
                }
            }

            if (nextCheck.Count > 0)
            {
                checkObjects = new List<GameObject>(nextCheck);
            }
            else finish = true;
        }

        var rbL = left.GetComponent<Rigidbody>();
        var rbR = right.GetComponent<Rigidbody>();

        RigidOn(left);
        RigidOn(right);

        rbL.AddForce(-normal * objScr.power / 3, ForceMode.Impulse);
        rbR.AddForce(normal * objScr.power / 3, ForceMode.Impulse);

        ShardSettings(left, objScr.shardDamage_nonDiv / 2);
        ShardSettings(right, objScr.shardDamage_nonDiv / 2);

        //破壊時のアクションを動作させるために少し遅延させる
        Invoke("SetNonActive", 0.1f);
    }

    private void SetNonActive()
    {
        gameObject.SetActive(false);
    }

    //おはようブラスト
    private void MorBlaBreak(GameObject obj)     //MPはおはようブラストのposition
    {
        var dir = (obj.transform.position - objScr.player.transform.position);
        dir.y = 0f;
        dir = dir.normalized;
        var F = dir * objScr.power;
        Vector3 TorquePower = new Vector3(Random.Range(-objScr.torque, objScr.torque), Random.Range(-objScr.torque, objScr.torque), Random.Range(-objScr.torque, objScr.torque));
        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
    }

    //ヒップドロップ
    private void FallenCollapse(GameObject obj)
    {
        RigidOn(obj);

        var direction = (obj.transform.position - objScr.player.transform.position);
        var rb = obj.GetComponent<Rigidbody>();
        var F = direction * objScr.power;
        Vector3 TorquePower = new Vector3(Random.Range(-objScr.torque, objScr.torque), Random.Range(-objScr.torque, objScr.torque), Random.Range(-objScr.torque, objScr.torque));
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
    }

    //落下カッター
    private void FallenCutterCollapse()
    {

    }
}
