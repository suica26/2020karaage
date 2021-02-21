using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreak_Y : MonoBehaviour
{
    public GameObject BreakEffect;
    public AudioClip ExplosionSound, CollapseSound;
    public Vector3 chainStartPos;
    public float deleteTime = 3f;   //破片消滅までの時間
    public float Torque = 1f;    //爆発でどれだけ回転するか
    public float Power = 1f;     //爆発でどれぐらい吹っ飛ぶか
    public float chainProbability = 5f;        //連鎖破壊発生確率
    public int chainDamage;                 //連鎖破壊でのダメージ(自分の破片)
    public int superChainDamage;    //ためキックによる連鎖破壊でのダメージ
    public int hitSkilID = 0;
    public float chainPower = 0f;
    public bool live = true;
    public bool death = false;
    private AudioSource audioSource;
    private GameObject player;

    // 自身の子要素を管理するリスト
    private List<GameObject> myParts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // 自分の子要素をチェック
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            // 子要素リストにパーツを追加
            myParts.Add(child.gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        tag = "Untagged";
    }

    // Update is called once per frame
    void Update()
    {
        if(death && tag != "Broken")
        {
            ObjectBreak();
            if (hitSkilID != 4) Destroy(GetComponent<BoxCollider>());
            Destroy(this.gameObject, deleteTime);   //オブジェクト削除

            //エフェクト発生
            Instantiate(BreakEffect, transform.position, Quaternion.identity, transform);
        }
    }

    public void ObjectBreak()
    {
        //破壊済み状態にタグとレイヤーを変更
        tag = "Broken";
        this.gameObject.layer = LayerMask.NameToLayer("BrokenObject");
        //破壊時のおはようブラストやカッターの位置を取得
        GameObject morBla = GameObject.Find("MorningBlastSphere_Y(Clone)");
        GameObject cutter = GameObject.Find("Cutter(Clone)");
        var morBlaPos = new Vector3();
        var cutterPos = new Vector3();
        if (hitSkilID == 2) morBlaPos = morBla.transform.position;
        if (hitSkilID == 3) cutterPos = cutter.transform.position;

        //Gは建物の重心
        var G = new Vector3();
        var P = transform.position;

        foreach (GameObject obj in myParts)
        {
            G += obj.transform.position;
        }
        G /= myParts.Count;

        bool chain = false;
        if (Random.Range(0f, 1f) < chainProbability / 100f) { chain = true; Debug.Log("Chain"); }

        foreach (GameObject obj in myParts)
        {
            if (hitSkilID == 4) break;
            obj.GetComponent<Rigidbody>().isKinematic = false;
            if (obj.GetComponent<BoxCollider>() == null) obj.AddComponent<BoxCollider>();
            obj.layer = LayerMask.NameToLayer("Shard");
            if (chain) SetChain(obj, chainDamage);

            switch (hitSkilID)
            {
                case 1: KickCollapse(obj, P); break;
                case 2: MorBlaBreak(obj, morBlaPos); break;
                case 3: CutterBreak(obj, cutterPos, G); break;
                case 5: StandardExplosion(obj); break;
                default: Debug.Log("エラー:対応していない処理です"); break;
            }
        }

        if (hitSkilID == 4)
        {
            SetChain(this.gameObject, superChainDamage); ChargeKickBreak();
        }
    }

    private void StandardExplosion(GameObject obj)
    {
        if (live)
        {
            audioSource.PlayOneShot(ExplosionSound);
            if(CollapseSound != null) audioSource.PlayOneShot(CollapseSound);
            live = false;
        }
        Vector3 forcePower = new Vector3(Random.Range(-Power, Power), Random.Range(-Power * 0.2f, Power * 0.2f), Random.Range(-Power * 0.75f, Power * 0.75f));
        Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));
        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(forcePower, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
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

    private void KickCollapse(GameObject obj, Vector3 P)
    {
        var pos = obj.transform.position;
        var dir = pos - P;
        float power = 0f;
        if (dir.y < 0) power = -dir.y;
        dir.y = 0f;
        dir = dir.normalized;
        var F = dir * power;
        Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));
        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
        if (live) audioSource.PlayOneShot(CollapseSound); live = false;
    }

    private void MorBlaBreak(GameObject obj, Vector3 MP)     //MPはおはようブラストのposition
    {
        var pos = obj.transform.position;
        var F = pos - MP;
        Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));
        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
        if (live)
        {
            audioSource.PlayOneShot(ExplosionSound);
            if (CollapseSound != null) audioSource.PlayOneShot(CollapseSound);
            live = false;
        }
    }

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

    private void ChargeKickBreak()
    {
        var dir = transform.position - player.transform.position;
        dir.y = 0f;
        dir = dir.normalized;
        dir.y = 0.5f;
        var F = dir * 50f;
        Torque *= 1000f;
        Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
    }

    private void SetChain(GameObject obj, int shardDamage)
    {
        var chainScript = obj.AddComponent<ChainBreak_Y>();
        chainScript.chainDamage = shardDamage;
        chainScript.expStartPos = gameObject.transform.position;
        chainScript.Chain(obj);
    }

    public void ChainExplode(GameObject obj, Vector3 G)
    {
        if (live)
        {
            audioSource.PlayOneShot(ExplosionSound);
            if (CollapseSound != null) audioSource.PlayOneShot(CollapseSound);
            live = false;
        }
        var F = (obj.transform.position - chainStartPos).normalized * chainPower;
        F.x *= Random.Range(0.2f, 1.8f);
        F.z *= Random.Range(0.2f, 1.8f);
        var rb = obj.GetComponent<Rigidbody>();
        Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
    }
}
