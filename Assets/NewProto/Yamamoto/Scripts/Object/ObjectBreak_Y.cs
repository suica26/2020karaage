using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreak_Y : MonoBehaviour
{
    private ObjectStateManagement_Y objScr;
    public AudioClip ExplosionSound, CollapseSound;
    public bool live = true;
    public bool death = false;
    private AudioSource audioSource;
    private GameObject player;
    private CriAtomSource criAtomSource;
    private string cueName_BreakSound;

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
        Collapsions = new VoidFunc[6] { TramplingCollapse, KickCollapse, KickCollapse, KickCollapse, KickCollapse, KickCollapse };
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
            }
        }
    }

    public void BreakAction()
    {
        /*
        //破壊時のおはようブラストやカッターの位置を取得
        GameObject morBla = GameObject.Find("MorningBlastSphere_Y(Clone)");
        GameObject cutter = GameObject.Find("Cutter(Clone)");
        var morBlaPos = new Vector3();
        var cutterPos = new Vector3();
        if (objScr.hitSkilID == 2) morBlaPos = morBla.transform.position;
        if (objScr.hitSkilID == 3) cutterPos = cutter.transform.position;

        //Gは建物の重心
        var G = new Vector3();
        var P = transform.position;

        foreach (GameObject obj in myParts)
        {
            G += obj.transform.position;
        }
        G /= myParts.Count;
        */

        if (divided)
        {
            foreach (var shard in myParts)
            {
                ShardSettings(shard);
                Collapsions[objScr.hitSkilID](shard);
            }
            objScr.Delete();
        }
        else
        {
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

    private void ShardSettings(GameObject shard)
    {
        if (shard.GetComponent<BoxCollider>() == null) shard.AddComponent<BoxCollider>();
        shard.layer = LayerMask.NameToLayer("Shard");
    }

    private void StandardExplosion(GameObject obj)
    {
        if (live)
        {
            if (ExplosionSound != null) audioSource.PlayOneShot(ExplosionSound);
            if (CollapseSound != null) audioSource.PlayOneShot(CollapseSound);
            live = false;
        }
        Vector3 forcePower = new Vector3(Random.Range(-objScr.power, objScr.power), Random.Range(-objScr.power * 0.2f, objScr.power * 0.2f), Random.Range(-objScr.power * 0.75f, objScr.power * 0.75f));
        Vector3 TorquePower = new Vector3(Random.Range(-objScr.torque, objScr.torque), Random.Range(-objScr.torque, objScr.torque), Random.Range(-objScr.torque, objScr.torque));
        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(forcePower, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
        //エフェクト発生
        if (objScr.breakEffect != null) Instantiate(objScr.breakEffect, transform.position, Quaternion.identity, transform);
    }

    //基本的な爆発
    private IEnumerator StandardExplosionCoroutin(GameObject obj, Vector3 G)
    {
        var forceDir = (G - obj.transform.position).normalized;
        var F = forceDir * 5f;
        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(F, ForceMode.Impulse);
        rb.useGravity = false;

        yield return new WaitForSeconds(1f);

        rb.isKinematic = false;
        rb.useGravity = true;
        StandardExplosion(obj);
    }

    private Vector3 GetVectorXZNormalized(Vector3 A, Vector3 B)
    {
        var AB = A - B;
        AB = new Vector3(AB.x, 0, AB.z);
        return AB.normalized;
    }

    //踏み潰し攻撃
    private void TramplingCollapse(GameObject obj)
    {
        RigidOn(obj);
        GetComponent<Rigidbody>().AddForce(0, -objScr.power, 0);
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
    private void CutterBreak(GameObject obj, Vector3 CP, Vector3 G)   //CPはカッターのposition　y座標比較に利用
    {
        if (obj.transform.position.y > CP.y)
        {
            var force = new Vector3(Random.Range(-2f, 2f), 10f, Random.Range(-2f, 2f));
            obj.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
        else
        {
            obj.GetComponent<Rigidbody>().isKinematic = true;
        }
        StartCoroutine(StandardExplosionCoroutin(obj, G));
    }

    //おはようブラスト
    private void MorBlaBreak(GameObject obj, Vector3 MP)     //MPはおはようブラストのposition
    {
        var pos = obj.transform.position;
        var F = pos - MP;
        Vector3 TorquePower = new Vector3(Random.Range(-objScr.torque, objScr.torque), Random.Range(-objScr.torque, objScr.torque), Random.Range(-objScr.torque, objScr.torque));
        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
        if (live)
        {
            if (ExplosionSound != null) audioSource.PlayOneShot(ExplosionSound);
            if (CollapseSound != null) audioSource.PlayOneShot(CollapseSound);
            live = false;
        }
    }

    //ヒップドロップ
    private void FallenCollapse(GameObject obj, Vector3 P)
    {
        RigidOn(obj);

        var direction = obj.transform.position - P;
        var rb = obj.GetComponent<Rigidbody>();

        direction = direction.normalized;
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
