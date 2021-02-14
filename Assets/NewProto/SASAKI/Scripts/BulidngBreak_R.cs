using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulidngBreak_R : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private FoodMaker_R scrFood = null;
    [Range(0, 4), SerializeField] private int tier_WalkAttack;
    [Range(0, 4), SerializeField] private int tier_ChargeKick;
    public GameObject BreakEffect;
    public float Torque;
    public float Power;
    public AudioClip AttackSound, ExplosionSound, BreakSound;
    public float HP;        //Inspector上から設定できます。
    public int kickDamage;  //キックで与えるダメージ量
    public int blastDamage;
    public int cutterDamage;
    public int breakScore;  //建物を破壊したときに得られるスコア
    public int breakPoint;  //建物を破棄したときに得られるチャージポイント
    bool Bung = false;
    bool Collapse = true;

    private chickenKick_R scrKick;
    private EvolutionChicken_R scrEvo;

    // 自身の子要素を管理するリスト
    List<GameObject> myParts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        scrKick = Player.GetComponent<chickenKick_R>();
        scrEvo = Player.GetComponent<EvolutionChicken_R>();

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
                scrKick.chargePoint += breakPoint;
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
            Destroy(objBoxCollider);　　//BoxCollider削除
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //キックダメージ
        if (other.gameObject.name == "KickCollision")
        {
            if(scrKick.chargePoint >= 100 && scrEvo.EvolutionNum >= tier_ChargeKick)
            {
                HP = 0;
                scrKick.chargePoint = 0;
            }
            else
            {
                HP -= kickDamage;
            }
        }
        //ブラストダメージ
        if (other.gameObject.name == "MorningBlastSphere_Y(Clone)")
        {
            Debug.Log("Hit");
            HP -= blastDamage;
        }
        //カッターダメージ
        if (other.gameObject.name == "Cutter(Clone)")
        {
            HP -= cutterDamage;
        }

        //振動させる
        Shake(0.25f, 0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //踏みつぶし攻撃
        if (collision.gameObject.tag == "Player" && scrEvo.EvolutionNum >= tier_WalkAttack)
        {
            HP = 0;
        }
    }

    // 吹き飛ばしメソッド
    void Explode()
    {
        foreach (GameObject obj in myParts)
        {
            Vector3 forcePower = new Vector3(Random.Range(-Power, Power), Random.Range(-Power * 0.5f, Power * 0.5f), Random.Range(-Power * 0.5f, Power * 0.5f));
            Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));

            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<Rigidbody>().AddForce(forcePower, ForceMode.Impulse);
            obj.GetComponent<Rigidbody>().AddTorque(TorquePower, ForceMode.Impulse);
            //5秒後に消す
            Destroy(gameObject, 5.0f);
        }
    }
    //揺らすメソッド
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

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
}
