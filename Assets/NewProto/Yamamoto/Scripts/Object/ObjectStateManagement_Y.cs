using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManagement_Y : MonoBehaviour
{
    [Range(0, 4), SerializeField] private int tier_WalkAttack;
    [SerializeField] private GameObject divideObject;
    private string attackSoundName, contactSoundName, ExplosionSoundName;
    public int HP;                  //Inspector上から設定できます。
    [Header("ダメージ倍率")]
    public float kickMag;       //キックのダメージ倍率
    public float blastMag;      //ブラストのダメージ倍率
    public float cutterMag;     //カッターのダメージ倍率
    public float fallAttackMag;     //落下攻撃のダメージ倍率
    [Header("破壊時のスコア")] public int breakScore;       //建物を破壊したときに得られるスコア
    private CriAtomSource criAtomSource;
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
    public int objectID;
    public GameObject player { get; private set; }
    //加筆(佐々木)
    private CharaMoveRigid_R scrCharaMove;
    private FoodMaker_R scrFood;
    private chickenKick_R scrKick;
    private EvolutionChicken_R scrEvo;
    /// <summary>
    /// 0:踏み潰し攻撃
    /// 1:チキンキック
    /// 2:トサカカッター
    /// 3:おはようブラスト
    /// 4:ヒップドロップ
    /// 5:ドロップカッター
    /// </summary>
    public int hitSkilID { get; private set; }
    public GameObject breakEffect;
    public float deleteTime = 3f;
    public float torque;    //爆発でどれだけ回転するか
    public float power;     //爆発でどれぐらい吹っ飛ぶか
    private Stage1_Mission_M playerScrS1M;
    private bool livingFlg;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");

        //加筆(佐々木)
        scrCharaMove = player.GetComponent<CharaMoveRigid_R>();
        scrKick = player.GetComponent<chickenKick_R>();
        scrEvo = player.GetComponent<EvolutionChicken_R>();
        scrFood = GetComponent<FoodMaker_R>();
        criAtomSource = GetComponent<CriAtomSource>();

        playerScrS1M = player.GetComponent<Stage1_Mission_M>();
        livingFlg = true;
    }

    //通常攻撃
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "KickCollision": Damage(kickMag, 1); break;
            case "Cutter(Clone)": Damage(cutterMag, 2); break;
            case "MorningBlastSphere_Y(Clone)": Damage(blastMag, 3); break;
            case "fallAttackCircle(Clone)": Damage(fallAttackMag * scrCharaMove.damageBoost, 4); break;
        }
    }

    //踏み潰し攻撃
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && scrEvo.EvolutionNum >= tier_WalkAttack && livingFlg)
        {
            hitSkilID = 0;
            Death();
        }
    }

    private void Damage(float mag, int skill)
    {
        //すでに破壊済みの場合は何も起きないようにする
        if (!livingFlg) return;

        HP -= (int)(scrEvo.Status_ATK * mag);
        hitSkilID = skill;
        //生死判定
        LivingCheck();
    }

    private void LivingCheck()
    {
        if (HP > 0)     //単純にダメージを受けたときの処理
        {
            //おはようブラストとカッターの時
            if (hitSkilID == 2 || hitSkilID == 3)
            {
                SetContactCue();
                if (criAtomSource != null) criAtomSource.cueName = contactSoundName;
                criAtomSource?.Play(contactSoundName);
            }
            else //それ以外
            {
                SetAttackCue();
                if (criAtomSource != null) criAtomSource.cueName = attackSoundName;
                criAtomSource?.Play(attackSoundName);
            }

            //振動させる
            StartCoroutine(DoShake(0.25f, 0.1f));
        }
        else Death();    //破壊したときの処理
    }

    /// <summary>
    /// 1 信号機
    /// 2 ゴミ箱
    /// 3 マンホール
    /// 4 建物 0
    /// 5 車
    /// 6 ガスタンク
    /// 7　木
    /// 8 消火栓
    /// 9 信号機Big
    /// </summary>
    private void SetAttackCue()
    {
        switch (objectID)
        {
            case 0: attackSoundName = "BuildingContact00"; break;
            case 2: attackSoundName = "TrachcanContact00"; break;
            case 3: attackSoundName = "Contact_Manhole00"; break;
            case 4: attackSoundName = "BuildingContact00"; break;
            case 5: attackSoundName = "CarContact00"; break;
            case 6: attackSoundName = "BuildingContact00"; break;
            case 7: attackSoundName = "KickTree00"; break;
            case 8: attackSoundName = "FireHydrant00"; break;
            case 9: attackSoundName = "TrafficExplosion00"; break;
            default: attackSoundName = "TrashcanContact00"; break;
        }
    }

    private void SetContactCue()
    {
        switch (objectID)
        {
            case 0: contactSoundName = "BuildingContact00"; break;
            case 1: contactSoundName = "PoleContract00"; break;
            case 2: contactSoundName = "TrachcanContact00"; break;
            case 3: contactSoundName = "Contact_Manhole00"; break;
            case 4: contactSoundName = "BuildingContact00"; break;
            case 5: contactSoundName = "CarContact00"; break;
            case 6: contactSoundName = "BuildingContact00"; break;
            case 7: contactSoundName = "KickTree00"; break;
            case 8: contactSoundName = "Contact_FireHydrant00"; break;
            case 9: contactSoundName = "PoleContract00"; break;
            default: contactSoundName = "TrachcanContact00"; break;
        }
    }

    private void SetBreakCue()
    {
        switch (objectID)
        {
            case 0: ExplosionSoundName = "BuildingExplosion00"; break;
            case 1: ExplosionSoundName = "PoleExplosion00"; break;
            case 2: ExplosionSoundName = "Trashcan00"; break;
            case 3: ExplosionSoundName = "Manhole00"; break;
            case 4: ExplosionSoundName = "BuildingExplosion00"; break;
            case 5: ExplosionSoundName = "CarExplosion00"; break;
            case 6: ExplosionSoundName = "GasExplosion00"; break;
            case 7: ExplosionSoundName = "FallenTree00"; break;
            case 8: ExplosionSoundName = "FireHydrant00"; break;
            case 9: ExplosionSoundName = "PoleContract00"; break;
            default: ExplosionSoundName = "PoleExplosion00"; break;
        }
    }

    //振動コルーチン
    private IEnumerator DoShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime)
        {
            var x = pos.x + Random.Range(-magnitude, magnitude);
            var y = pos.y + Random.Range(-magnitude, magnitude);
            var z = pos.z + Random.Range(-magnitude, magnitude);

            transform.localPosition = new Vector3(x, y, z);

            yield return null;
        }

        transform.localPosition = pos;
    }

    private void Death()
    {
        //一応、HPは0になるように補正をかける
        HP = 0;

        GameObject.Find("Canvas").GetComponent<Parameters_R>().ScoreManager(breakScore);

        SetBreakCue();
        if (criAtomSource != null) criAtomSource.cueName = ExplosionSoundName;
        criAtomSource?.Play(ExplosionSoundName);

        //餌のスポーン処理
        scrFood?.DropFood();

        DeathCount();

        //差し替え処理
        if (divideObject != null) ChangeObject();
        else Substitution();    //差し替えをしない場合

        livingFlg = false;
    }

    public void DeathCount()
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

    //差し替えする場合
    private void ChangeObject()
    {
        var genPos = transform.position;
        genPos.y = 0f;
        var dividedObject = Instantiate(divideObject, genPos, transform.rotation);
        var breakScr = dividedObject.AddComponent<ObjectBreak_Y>();
        breakScr.InitSetting(this, true);
        breakScr.BreakAction();

        Destroy(gameObject, deleteTime);
        gameObject.SetActive(false);
    }
    //差し替えしない場合
    private void Substitution()
    {
        var breakScr = gameObject.AddComponent<ObjectBreak_Y>();
        breakScr.InitSetting(this, false);
        breakScr.BreakAction();
        foreach (var colliders in GetComponents<Collider>())
        {
            colliders.enabled = false;
        }
        Destroy(gameObject, deleteTime);
    }

    public void Delete()
    {
        Destroy(gameObject, deleteTime);
    }
}