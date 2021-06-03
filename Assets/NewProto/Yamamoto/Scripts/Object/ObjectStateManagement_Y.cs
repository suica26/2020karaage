using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManagement_Y : MonoBehaviour
{
    [Range(0, 4), SerializeField] private int tier_WalkAttack;
    [Range(0, 4), SerializeField] private int tier_ChargeKick;
    public GameObject divideObject = null;
    private string attackSoundName, contactSoundName, ExplosionSoundName;
    public int HP;                  //Inspector上から設定できます。
    [Header("ダメージ倍率")]
    public float kickMag;             //キックのダメージ倍率
    public float blastMag;         //ブラストのダメージ倍率
    public float cutterMag;        //カッターのダメージ倍率
    public float fallAttackMag;     //落下攻撃のダメージ倍率
    [Header("破壊時のスコア")] public int breakScore;                          //建物を破壊したときに得られるスコア
    [Header("破壊時のチャージポイント")] public int breakPoint;                //建物を破壊したときに得られるチャージポイント
    private CriAtomSource criAtomSource;
    /// <summary>
    /// 1 木
    /// 2 消火栓
    /// 3 マンホール
    /// 4 建物
    /// 5 車
    /// </summary>
    [SerializeField] private int objectID;
    private GameObject player;

    //M
    //[SerializeField] private int smallObj,bigObj;
    private Stage1_Mission_M s1Mis;//

    //加筆(佐々木)
    private CharaMoveRigid_R scrCharaMove;

    private Vector3 chainStartPos;
    private FoodMaker_R scrFood;
    private chickenKick_R scrKick;
    private EvolutionChicken_R scrEvo;
    private float chainPower;
    private int hitSkilID = 0;
    private bool damage = false;
    //M publicに変えました
    public bool notLive = false;

    [Header("破壊時の設定")]
    public GameObject BreakEffect;
    public float deleteTime = 3f;
    public float Torque = 1f;    //爆発でどれだけ回転するか
    public float Power = 1f;     //爆発でどれぐらい吹っ飛ぶか
    [Header("連鎖破壊でのダメージ量(相手への)")] public int chainDamage;                 //連鎖破壊でのダメージ(自分の破片)

    private Stage1_Mission_M playerScrS1M;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //M
        s1Mis = GetComponent<Stage1_Mission_M>();
        //smallObj = s1Mis.smallNum;
        //bigObj = s1Mis.bigNum;//

        //加筆(佐々木)
        scrCharaMove = player.GetComponent<CharaMoveRigid_R>();
        //
        scrKick = player.GetComponent<chickenKick_R>();
        scrEvo = player.GetComponent<EvolutionChicken_R>();
        scrFood = GetComponent<FoodMaker_R>();
        criAtomSource = GetComponent<CriAtomSource>();

        playerScrS1M = player.GetComponent<Stage1_Mission_M>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0 && !notLive)//HPがなくなると、分割オブジェクトに差し替え発生
        {
            GameObject.Find("Canvas").GetComponent<Parameters_R>().ScoreManager(breakScore);
            scrKick.chargePoint += breakPoint;

            switch (objectID)
            {
                case 0: ExplosionSoundName = "BuildingExplosion00"; break;
                case 1: ExplosionSoundName = "PoleExplosion00"; break;
                case 2: ExplosionSoundName = "Trashcan00"; break;
                case 3: ExplosionSoundName = "PoleExplosion00"; break;
                case 4: ExplosionSoundName = "BuildingExplosion00"; break;
                case 5: ExplosionSoundName = "CarExplosion00"; break;
                case 6: ExplosionSoundName = "GasExplosion00"; break;
                case 7: ExplosionSoundName = "FallenTree00"; break;
                case 8: ExplosionSoundName = "FireHydrant00"; break;
                default: ExplosionSoundName = "PoleExplosion00"; break;
            }

            if (criAtomSource != null) criAtomSource.cueName = ExplosionSoundName;
            criAtomSource?.Play(ExplosionSoundName);

            if (scrFood != null)
            {
                scrFood.DropFood();
            }

            //差し替え処理
            if (divideObject != null)
            {
                ChangeObject();
            }
            else     //差し替えをしない場合
            {
                Substitution();
            }

            DeathCount();
            notLive = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //キックダメージ
        if (other.gameObject.name == "KickCollision")
        {
            HP -= (int)(scrEvo.Status_ATK * kickMag);
            hitSkilID = 1;
            damage = true;
        }
        //ブラストダメージ
        if (other.gameObject.name == "MorningBlastSphere_Y(Clone)")
        {
            HP -= (int)(scrEvo.Status_ATK * blastMag / 3f);
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
        if (other.gameObject.name == "fallAttackCircle(Clone)")
        {
            //加筆しました(元スクリプト：(int)(scrEvo.Status_ATK * fallAttackMag);)
            HP -= (int)(scrEvo.Status_ATK * fallAttackMag * scrCharaMove.damageBoost);
            hitSkilID = 6;
            damage = true;
        }

        if (damage)
        {
            /// <summary>
            /// 1 信号機
            /// 2 ゴミ箱
            /// 3 マンホール
            /// 4 建物 0
            /// 5 車
            /// 6 ガスタンク
            /// 7　木
            /// 8 消火栓
            /// </summary>

            //おはようブラストとカッターの時
            if (hitSkilID == 2 || hitSkilID == 3)
            {
                switch (objectID)
                {
                    case 0: contactSoundName = "BuildingContact00"; break;
                    case 1: contactSoundName = "PoleContract00"; break;
                    case 2: contactSoundName = "TrachcanContact00"; break;
                    case 3: contactSoundName = "TrachcanContact00"; break;
                    case 4: contactSoundName = "BuildingContact00"; break;
                    case 5: contactSoundName = "CarContact00"; break;
                    case 6: contactSoundName = "BuildingContact00"; break;
                    case 7: contactSoundName = "KickTree00"; break;
                    case 8: contactSoundName = "Contact_FireHydrant00"; break;
                    default: contactSoundName = "TrachcanContact00"; break;
                }

                if (criAtomSource != null) criAtomSource.cueName = contactSoundName;
                criAtomSource?.Play(contactSoundName);
            }
            else //それ以外
            {
                switch (objectID)
                {
                    case 0: attackSoundName = "BuildingContact00"; break;
                    case 1: attackSoundName = "PoleContract00"; break;
                    case 2: attackSoundName = "TrachcanContact00"; break;
                    case 3: attackSoundName = "TrachcanContact00"; break;
                    case 4: attackSoundName = "BuildingContact00"; break;
                    case 5: attackSoundName = "CarContact00"; break;
                    case 6: attackSoundName = "BuildingContact00"; break;
                    case 7: attackSoundName = "KickTree00"; break;
                    case 8: attackSoundName = "Contact_FireHydrant00"; break;
                    default: attackSoundName = "TrachcanContact00"; break;
                }

                if (criAtomSource != null) criAtomSource.cueName = attackSoundName;
                criAtomSource?.Play(attackSoundName);
            }
            //振動させる
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

            switch (objectID)
            {
                case 0: attackSoundName = "BuildingContact00"; break;
                case 2: attackSoundName = "TrashcanContact00"; break;
                case 3: attackSoundName = "TrashcanContact00"; break;
                case 4: attackSoundName = "BuildingContact00"; break;
                case 5: attackSoundName = "CarContact00"; break;
                case 6: attackSoundName = "BuildingContact00"; break;
                case 7: attackSoundName = "KickTree00"; break;
                default: attackSoundName = "TrashcanContact00"; break;
            }

            //nullチェック
            if (criAtomSource != null) criAtomSource.cueName = attackSoundName;
            criAtomSource?.Play(attackSoundName);
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

    //差し替えする場合
    private void ChangeObject()
    {
        var genPos = transform.position;
        genPos.y = 0f;
        var DO = Instantiate(divideObject, genPos, transform.rotation);
        var rb = DO.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        var scr = DO.AddComponent<ObjectBreak_Y>();
        scr.hitSkilID = hitSkilID;
        scr.chainPower = chainPower;
        scr.chainStartPos = chainStartPos;
        scr.BreakEffect = BreakEffect;
        scr.deleteTime = deleteTime;
        scr.Torque = Torque;
        scr.Power = Power;
        scr.chainProbability = 5f;
        scr.chainDamage = chainDamage;
        scr.death = true;
        Destroy(this.gameObject);
    }
    //しない場合
    private void Substitution()
    {
        var scr = this.gameObject.AddComponent<ObjectBreak_Y>();
        scr.hitSkilID = hitSkilID;
        scr.chainPower = chainPower;
        scr.chainStartPos = chainStartPos;
        scr.BreakEffect = BreakEffect;
        scr.deleteTime = deleteTime;
        scr.Torque = Torque;
        scr.Power = Power;
        scr.chainProbability = 5f;
        scr.chainDamage = chainDamage;
        scr.death = true;
    }

    private void DeathCount()
    {
        if (this.gameObject.tag == "Small")
        {
            //山本加筆　元:player.GetComponent<Stage1_Mission_M>().SmallNumberPlus();
            playerScrS1M.SmallNumberPlus();
        }
        else if (this.gameObject.tag == "Big")
        {
            //山本加筆　元:player.GetComponent<Stage1_Mission_M>().BigNumberPlus();
            playerScrS1M.BigNumberPlus();
        }
    }
}