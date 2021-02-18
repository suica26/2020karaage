using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBreak_Y : MonoBehaviour
{
    [Range(0, 4), SerializeField] private int tier_WalkAttack;
    [Range(0, 4), SerializeField] private int tier_ChargeKick;
    public GameObject BreakEffect;
    public AudioClip AttackSound, ExplosionSound, BreakSound;
    public int HP;                  //Inspector上から設定できます。
    public float Torque;    //爆発でどれだけ回転するか
    public float Power;     //爆発でどれぐらい吹っ飛ぶか
    [Header("ダメージ倍率")]
    public float kickMag;             //キックのダメージ倍率
    public float blastMag;         //ブラストのダメージ倍率
    public float cutterMag;        //カッターのダメージ倍率
    public float chargeKickMag;       //ためキックのダメージ倍率
    [Header("連鎖破壊発生確率")] [Range(0, 100)] public float chainProbability = 5f;        //連鎖破壊発生確率
    [Header("連鎖破壊でのダメージ量")] public int chainDamage;                 //連鎖破壊でのダメージ(自分の破片)
    [Header("ためキックによる連鎖破壊でのダメージ量")] public int superChainDamage;    //ためキックによる連鎖破壊でのダメージ
    [Header("破壊時のスコア")] public int breakScore;                          //建物を破壊したときに得られるスコア
    [Header("破壊時のチャージポイント")] public int breakPoint;                //建物を破壊したときに得られるチャージポイント
    private AudioSource aSound, eSound, bSound;
    private GameObject player;
    private Vector3 chainStartPos;
    private FoodMaker_R scrFood;
    private chickenKick_R scrKick;
    private EvolutionChicken_R scrEvo;
    private int hitSkilID = 0;
    private float deleteTime = 3f;
    private float chainPower = 0f;
    private bool damage = false;
    private bool Bung = false;
    private bool explosion = false;
    private bool finish = false;

    // 自身の子要素を管理するリスト
    List<GameObject> myParts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        scrKick = player.GetComponent<chickenKick_R>();
        scrEvo = player.GetComponent<EvolutionChicken_R>();
        scrFood = GetComponent<FoodMaker_R>();
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
        hitSkilID = 4;

        if (HP <= 0 && !Bung)//ダメージがHPを超えると破壊
        {
            GameObject.Find("Canvas").GetComponent<Parameters_R>().ScoreManager(breakScore);
            scrKick.chargePoint += breakPoint;

            if (scrFood != null)
            {
                scrFood.DropFood();
            }

            BuildingBreak();
            if (hitSkilID != 4) Destroy(GetComponent<BoxCollider>());
            Destroy(this.gameObject, deleteTime);   //オブジェクト削除
            Bung = true;
        }

        if (explosion && !finish)
        {
            //エフェクト発生
            Instantiate(BreakEffect, transform.position, Quaternion.identity, transform);
            //サウンド再生
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
            if (scrKick.chargePoint >= 100)
            {
                if(scrEvo.EvolutionNum >= tier_ChargeKick)
                {
                    HP = 0;
                    scrKick.chargePoint = 0;
                    hitSkilID = 4;
                }
                else
                {
                    HP -= (int)(scrEvo.Status_ATK * chargeKickMag);
                }
            }
            else
            {
                HP -= (int)(scrEvo.Status_ATK * kickMag);
                hitSkilID = 1;
            }
            damage = true;
        }
        //ブラストダメージ
        if (other.gameObject.name == "MorningBlastSphere_Y(Clone)")
        {
            HP -= (int)(scrEvo.Status_ATK * blastMag);
            hitSkilID = 2;
            damage = true;
        }
        //カッターダメージ
        if (other.gameObject.name == "Cutter(Clone)")
        {
            HP -= (int)(scrEvo.Status_ATK * cutterMag);
            hitSkilID = 3;
            damage = true;
        }
        if (other.gameObject.tag == "Chain")
        {
            var chainScript = other.gameObject.GetComponent<ChainBreak_Y>();
            HP -= chainScript.chainDamage;
            chainStartPos = chainScript.expStartPos;
            chainPower = other.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 0.8f;
            hitSkilID = 0;
            damage = true;
        }

        if (damage)
        {
            //振動させる
            aSound.PlayOneShot(AttackSound);
            StartCoroutine(DoShake(0.25f, 0.1f));
            damage = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //踏みつぶし攻撃
        if (collision.gameObject.tag == "Player" && scrEvo.EvolutionNum >= tier_WalkAttack)
        {
            HP = 0;
            hitSkilID = 5;
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

    // 破壊メソッド
    void BuildingBreak()
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
        if (Random.Range(0f, 1f) < chainProbability/100f) { chain = true; Debug.Log("Chain"); }

        foreach (GameObject obj in myParts)
        {
            if (hitSkilID == 4) break;
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.AddComponent<BoxCollider>();
            obj.layer = LayerMask.NameToLayer("Shard");
            if (chain) SetChain(obj,chainDamage);

            switch (hitSkilID)
            {
                case 1: KickCollapse(obj, P); break;
                case 2: MorBlaBreak(obj, morBlaPos); break;
                case 3: CutterBreak(obj, cutterPos, G); break;
                case 5: StandardExplosion(obj); break;
                default: Debug.Log("エラー:対応していない処理です"); break;
            }

            //5秒後に消す
            Destroy(obj, deleteTime);
        }

        if (hitSkilID == 4)
        {
            SetChain(this.gameObject, superChainDamage); ChargeKickBreak();
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

    private void SetChain(GameObject obj,int shardDamage)
    {
        var chainScript = obj.AddComponent<ChainBreak_Y>();
        chainScript.chainDamage = shardDamage;
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
