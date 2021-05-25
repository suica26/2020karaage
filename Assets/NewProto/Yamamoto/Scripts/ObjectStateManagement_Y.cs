using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManagement_Y : MonoBehaviour
{
    [Range(0, 4), SerializeField] private int tier_WalkAttack;
    [Range(0, 4), SerializeField] private int tier_ChargeKick;
    public GameObject divideObject = null;
    public AudioClip AttackSound, ContactSound;
    public int HP;                  //Inspector上から設定できます。
    [Header("ダメージ倍率")]
    public float kickMag;             //キックのダメージ倍率
    public float blastMag;         //ブラストのダメージ倍率
    public float cutterMag;        //カッターのダメージ倍率
    public float fallAttackMag;     //落下攻撃のダメージ倍率
    [Header("破壊時のスコア")] public int breakScore;                          //建物を破壊したときに得られるスコア
    [Header("破壊時のチャージポイント")] public int breakPoint;                //建物を破壊したときに得られるチャージポイント
    private AudioSource audioSource;
    private GameObject player;

    //M
    //[SerializeField] private int smallObj,bigObj;
    private Stage1_Mission_M s1Mis;//

    //加筆(佐々木)
    private CharaMoveRigid_R scrCharaMove;
    //

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
    public AudioClip ExplosionSound, CollapseSound;
    public float deleteTime = 3f;
    public float Torque = 1f;    //爆発でどれだけ回転するか
    public float Power = 1f;     //爆発でどれぐらい吹っ飛ぶか
    [Header("連鎖破壊でのダメージ量(相手への)")] public int chainDamage;                 //連鎖破壊でのダメージ(自分の破片)

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
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (HP <= 0 && !notLive)//HPがなくなると、分割オブジェクトに差し替え発生
        {
            GameObject.Find("Canvas").GetComponent<Parameters_R>().ScoreManager(breakScore);
            scrKick.chargePoint += breakPoint;

            //M 
            if (this.gameObject.tag == "Small")
            {
                player.GetComponent<Stage1_Mission_M>().SmallNumberPlus();
                //smallObj++;
            }
            else if (this.gameObject.tag == "Big")
            {
                player.GetComponent<Stage1_Mission_M>().BigNumberPlus();
                //bigObj++;
            }//

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
            if (hitSkilID == 2 || hitSkilID == 3)
            {
                if (ContactSound != null) audioSource.PlayOneShot(ContactSound);
            }
            else
            {
                if (AttackSound != null) audioSource.PlayOneShot(AttackSound);
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
            audioSource.PlayOneShot(AttackSound);
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
        DO.AddComponent<AudioSource>();
        var rb = DO.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        var scr = DO.AddComponent<ObjectBreak_Y>();
        scr.hitSkilID = hitSkilID;
        scr.chainPower = chainPower;
        scr.chainStartPos = chainStartPos;
        scr.ExplosionSound = ExplosionSound;
        scr.CollapseSound = CollapseSound;
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
        scr.ExplosionSound = ExplosionSound;
        scr.CollapseSound = CollapseSound;
        scr.BreakEffect = BreakEffect;
        scr.deleteTime = deleteTime;
        scr.Torque = Torque;
        scr.Power = Power;
        scr.chainProbability = 5f;
        scr.chainDamage = chainDamage;
        scr.death = true;
    }
}
