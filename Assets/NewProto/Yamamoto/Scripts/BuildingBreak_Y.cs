using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBreak_Y : MonoBehaviour
{
    [SerializeField] private FoodMaker_R scrFood = null;
    public GameObject BreakEffect;
    public float Torque;
    public float Power;
    public AudioClip AttackSound, ExplosionSound, BreakSound;
    private AudioSource aSound, eSound, bSound;
    public int HP;        //Inspector上から設定できます。
    public int kickDamage;  //キックで与えるダメージ量
    public int blastDamage;
    public int cutterDamage;
    public int breakScore;  //建物を破壊したときに得られるスコア
    bool Bung = false;
    private bool damage = false;
    private int hitSkilID = 0;
    private float deleteTime = 3f;
    private Vector3 chainStartPos;
    private float chainPower = 0f;
    private bool explosion = false;
    private bool finish = false;

    // 自身の子要素を管理するリスト
    List<GameObject> myParts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // 自分の子要素をチェック
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            // 子要素リストにパーツを追加
            myParts.Add(child.gameObject);
        }
        aSound = GetComponent<AudioSource>();
        eSound = GetComponent<AudioSource>();
        bSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0 && !Bung)//ダメージがHPを超えると破壊
        {
            GameObject.Find("Canvas").GetComponent<Parameters_R>().ScoreManager(breakScore);

            if (scrFood != null)
            {
                scrFood.DropFood();
            }
            Explode();

            Destroy(this.gameObject.GetComponent<BoxCollider>());
            Destroy(this.gameObject, deleteTime);   //オブジェクト削除
            Bung = true;
        }

        if (explosion && !finish)
        {
            //エフェクト発生
            Instantiate(BreakEffect, transform.position, Quaternion.identity, transform);
            //サウンド再生
            aSound.PlayOneShot(AttackSound);
            eSound.PlayOneShot(ExplosionSound);
            bSound.PlayOneShot(BreakSound);
            finish = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //キックダメージ
        if (other.gameObject.name == "KickCollision")
        {
            HP -= kickDamage;
            hitSkilID = 1;
            damage = true;
        }
        //ブラストダメージ
        if (other.gameObject.name == "MorningBlastSphere_Y(Clone)")
        {
            HP -= blastDamage;
            hitSkilID = 2;
            damage = true;
        }
        //カッターダメージ
        if (other.gameObject.name == "Cutter(Clone)")
        {
            HP -= cutterDamage;
            hitSkilID = 3;
            damage = true;
        }
        if (other.gameObject.tag == "Chain")
        {
            var chainScript = other.gameObject.GetComponent<ChainBreak_Y>();
            HP -= chainScript.chainDamage;
            chainStartPos = chainScript.expStartPos;
            chainPower = other.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 0.8f;
            hitSkilID = 8;
            damage = true;
        }

        if (damage)
        {
            //振動させる
            StartCoroutine(DoShake(0.25f, 0.1f));
            damage = false;
        }
    }

    //振動コルーチン
    private IEnumerator DoShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = pos;
    }

    // 吹き飛ばしメソッド
    void Explode()
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
        if (Random.Range(0f, 1f) < 0.05f) { chain = true; Debug.Log("Chain"); }

        foreach (GameObject obj in myParts)
        {
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.AddComponent<BoxCollider>();
            obj.layer = LayerMask.NameToLayer("Shard");
            if (chain) SetChain(obj);

            if (hitSkilID == 1)
            {
                KickCollapse(obj, P);
            }
            else if (hitSkilID == 2)
            {
                MorBlaBreak(obj, morBlaPos);
            }
            else if (hitSkilID == 3)
            {
                CutterBreak(obj, cutterPos, G);
            }
            else
            {
                ChainExplode(obj, G);
            }
            //5秒後に消す
            Destroy(obj, deleteTime);
        }
    }

    private void StandardExplosion(GameObject obj)
    {
        explosion = true;
        Vector3 forcePower = new Vector3(Random.Range(-Power, Power), Random.Range(-Power * 0.2f, Power * 0.2f), Random.Range(-Power * 0.75f, Power * 0.75f));
        Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));
        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(forcePower, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
    }

    //基本的な爆発
    private IEnumerator StandardExplosionCoroutin(GameObject obj,Vector3 G)
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
        explosion = true;
    }

    private void MorBlaBreak(GameObject obj,Vector3 MP)     //MPはおはようブラストのposition
    {
        var pos = obj.transform.position;
        var F = pos - MP;
        Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));
        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
        explosion = true;
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

    private void SetChain(GameObject obj)
    {
        var chainScript = obj.AddComponent<ChainBreak_Y>();
        chainScript.expStartPos = gameObject.transform.position;
        chainScript.Chain(obj);
    }

    public void ChainExplode(GameObject obj,Vector3 G)
    {
        explosion = true;
        var F = (obj.transform.position - chainStartPos).normalized * chainPower;
        F.x *= Random.Range(0.2f, 1.8f);
        F.z *= Random.Range(0.2f, 1.8f);
        var rb = obj.GetComponent<Rigidbody>();
        Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
    }
}
