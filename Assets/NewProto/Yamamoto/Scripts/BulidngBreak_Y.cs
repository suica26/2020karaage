using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulidngBreak_Y : MonoBehaviour
{
    [SerializeField] private FoodMaker_R scrFood = null;
    public GameObject BreakEffect;
    public float Torque;
    public float Power;
    public AudioClip AttackSound, ExplosionSound, BreakSound;
    public float HP;        //Inspector上から設定できます。
    public int kickDamage;  //キックで与えるダメージ量
    public int blastDamage;
    public int cutterDamage;
    public int breakScore;  //建物を破壊したときに得られるスコア
    bool Bung = false;
    bool Collapse = true;
    private int hitSkilID = 0;
    private float deleteTime = 3f;
    private Vector3 chainStartPos;
    private float chainPower = 0f;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)//ダメージがHPを超えると破壊
        {
            if (!Bung)
            {
                GameObject.Find("Canvas").GetComponent<Parameters_R>().ScoreManager(breakScore);
            }
            Bung = true;
        }

        if (Bung && Collapse)
        {
            Collapse = false;
            if(scrFood != null)
            {
                scrFood.DropFood();
            }
            Explode();
            Instantiate(BreakEffect, transform.position, Quaternion.identity, transform);　//エフェクト発生

            //サウンド再生
            AudioSource sound1 = GetComponent<AudioSource>();
            sound1.PlayOneShot(AttackSound);
            AudioSource sound2 = GetComponent<AudioSource>();
            sound2.PlayOneShot(ExplosionSound);
            AudioSource sound3 = GetComponent<AudioSource>();
            sound3.PlayOneShot(BreakSound);

            var objBoxCollider = gameObject.GetComponent<BoxCollider>();　　//BoxCollider削除用インスタンス作成
            Destroy(this.gameObject, deleteTime);　　//BoxCollider削除
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //キックダメージ
        if (other.gameObject.name == "KickCollision")
        {
            HP -= kickDamage;
            hitSkilID = 1;
        }
        //ブラストダメージ
        if (other.gameObject.name == "MorningBlastSphere_Y(Clone)")
        {
            Debug.Log("Hit");
            HP -= blastDamage;
            hitSkilID = 2;
        }
        //カッターダメージ
        if (other.gameObject.name == "Cutter(Clone)")
        {
            HP -= cutterDamage;
            hitSkilID = 3;
        }
        if (other.gameObject.tag == "Chain")
        {
            var chainScript = other.gameObject.GetComponent<ChainBreak_Y>();
            HP -= chainScript.chainDamage;
            chainStartPos = chainScript.expStartPos;
            chainPower = other.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 0.8f;
            hitSkilID = 8;
        }

        //振動させる
        StartCoroutine(DoShake(0.25f, 0.1f));
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
        this.gameObject.tag = "Broken";
        GameObject cutter = GameObject.Find("Cutter(Clone)");
        var cutterPos = new Vector3();
        if (hitSkilID == 3) cutterPos = cutter.transform.position;

        var G = new Vector3();

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
            if (chain) SetChain(obj);

            if(hitSkilID == 1)
            {
                StartCoroutine(StandardExplosionCoroutin(obj, G));
            }
            else if (hitSkilID == 3)
            {
                CutterExplode(obj, cutterPos, G);
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

    public void CutterExplode(GameObject obj, Vector3 CP, Vector3 G)
    {
        if (obj.transform.position.y > CP.y)
        {
            var force = new Vector3(Random.Range(-2f, 2f), 10f, Random.Range(-2f, 2f));
            obj.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            StartCoroutine(StandardExplosionCoroutin(obj, G));
        }
        else
        {
            obj.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(StandardExplosionCoroutin(obj, G));
        }
    }

    private void SetChain(GameObject obj)
    {
        var chainScript = obj.AddComponent<ChainBreak_Y>();
        chainScript.expStartPos = gameObject.transform.position;
        chainScript.Chain(obj);
    }

    public void ChainExplode(GameObject obj,Vector3 G)
    {
        var F = (obj.transform.position - chainStartPos).normalized * chainPower;
        F.x *= Random.Range(0.2f, 1.8f);
        F.z *= Random.Range(0.2f, 1.8f);
        var rb = obj.GetComponent<Rigidbody>();
        Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));
        rb.AddForce(F, ForceMode.Impulse);
        rb.AddTorque(TorquePower, ForceMode.Impulse);
    }
}
