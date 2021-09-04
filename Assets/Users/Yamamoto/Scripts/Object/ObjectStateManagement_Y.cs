using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManagement_Y : MonoBehaviour
{
    [Range(0, 4), SerializeField] protected int tier_WalkAttack;
    [SerializeField] private GameObject divideObject;
    protected string attackSoundName, contactSoundName, ExplosionSoundName, CutterContactSoundName;
    protected int MaxHP;
    public int HP;                  //Inspector上から設定できます。
    [Header("ダメージ倍率")]
    public float kickMag;       //キックのダメージ倍率
    public float blastMag;      //ブラストのダメージ倍率
    public float cutterMag;     //カッターのダメージ倍率
    public float fallAttackMag;     //落下攻撃のダメージ倍率
    [Header("破壊時のスコア")] public int breakScore;       //建物を破壊したときに得られるスコア
    protected CriAtomSource criAtomSource;
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
    protected CharaMoveRigid_R scrCharaMove;
    protected FoodMaker_R scrFood;
    protected EvolutionChicken_R scrEvo;
    //加筆　undertreem 0625
    protected ADX_SoundRaycast ADX_RevLevel;
    protected string Rev; //ADXバス名
    /// <summary>
    /// 0:踏み潰し攻撃
    /// 1:チキンキック
    /// 2:トサカカッター
    /// 3:おはようブラスト
    /// 4:ヒップスタンプ
    /// </summary>
    public int hitSkilID { get; private set; }
    public GameObject breakEffect;
    public float deleteTime = 3f;
    public float torque;    //爆発でどれだけ回転するか
    public float power;     //爆発でどれぐらい吹っ飛ぶか
    //M Missions_M（親）だと効果なし←そんなことなかった
    protected Missions_M missionScr;
    protected bool livingFlg = true;
    //破壊後のオブジェクトが地面(等)に接触したときの音
    public string groundContactSoundName;
    protected Renderer[] renderers;
    public int shardDamage_nonDiv;
    public bool specialObjectFlg;
    [SerializeField] protected bool notDamage;
    public float magnitude = 0.1f;
    public float duration = 0.25f;

    //Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.Find("Player");

        //加筆(佐々木)
        scrCharaMove = player.GetComponent<CharaMoveRigid_R>();
        scrEvo = player.GetComponent<EvolutionChicken_R>();
        scrFood = GetComponent<FoodMaker_R>();
        criAtomSource = GetComponent<CriAtomSource>();
        //加筆　undertreem 0625
        ADX_RevLevel = player.GetComponent<ADX_SoundRaycast>();
        renderers = CheckRenderer();

        MaxHP = HP;
    }
    public void changeDamageFlg()
    {
        notDamage = false;
    }

    //通常攻撃
    protected void OnTriggerEnter(Collider other)
    {
        if (!notDamage)
        {
            switch (other.gameObject.name)
            {
                case "KickCollision": Damage(kickMag, 1); break;
                case "Cutter(Clone)": Damage(cutterMag, 2); break;
                case "MorningBlastSphere_Y(Clone)": Damage(blastMag, 3); break;
                case "fallAttackCircle(Clone)": Damage(fallAttackMag * scrCharaMove.damageBoost, 4); break;
            }
        }
    }

    //踏み潰し攻撃などなど
    protected virtual void OnCollisionEnter(Collision collision)
    {
        //踏み潰し攻撃が発生したとき
        bool trampling = collision.gameObject.tag == "Player" && scrEvo.EvolutionNum >= tier_WalkAttack;
        //破砕車がぶつかったとき
        bool carHit = collision.gameObject.name.Contains("car") && collision.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude > 5f;
        //ガスタンクがぶつかったとき
        bool gasTankHit = collision.gameObject.name.Contains("mainTank") && collision.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude > 5f;

        //破砕車、ガスタンクが建物にぶつかったときキューを入れ替える
        if ((carHit || gasTankHit) && objectID == 0 && !specialObjectFlg)
        {
            objectID = 10;
        }
        //即死条件と生存フラグがどちらも成り立つとき、即死発動
        if ((trampling || carHit || gasTankHit) && livingFlg && !specialObjectFlg)
        {
            SetSkillID(0);
            Death();
        }
        //画面内のときのみ鳴らす
        if (!livingFlg && collision.gameObject.tag != "Player" && groundContactSoundName != null && CheckRenVisible())
        {
            if (groundContactSoundName != "")
                criAtomSource?.Play(groundContactSoundName);
        }
    }

    protected Renderer[] CheckRenderer()
    {
        //Rendererがついているか確認するリスト
        var checkObjects = new List<GameObject>();
        //最初は代入されたobjをとりあえず入れておく
        checkObjects.Add(gameObject);
        //返り値のRenderer
        var renderers = new List<Renderer>();

        bool finish = false;

        while (!finish)
        {
            var nextCheck = new List<GameObject>();
            foreach (var cObj in checkObjects)
            {
                //メッシュが設定されている(＝空オブジェクトでない)オブジェクトの場合
                if (cObj.GetComponent<Renderer>() != null)
                {
                    renderers.Add(cObj.GetComponent<Renderer>());
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

        return renderers.ToArray();
    }

    protected bool CheckRenVisible()
    {
        //構成Rendererのどれか一つでも画面内にあればtrueを返す
        foreach (var ren in renderers)
        {
            if (ren.isVisible) return true;
        }
        //全てが画面街にある時はfalseを返す
        return false;
    }

    public void SetSkillID(int num)
    {
        if (num < 0) hitSkilID = 0;
        else if (num > 4) hitSkilID = 4;
        else hitSkilID = num;
    }

    public virtual void Damage(float mag, int skill)
    {
        //すでに破壊済みの場合は何も起きないようにする
        if (!livingFlg) return;

        HP -= (int)(scrEvo.Status_ATK * mag);
        SetSkillID(skill);
        //生死判定
        LivingCheck();
    }

    public virtual void LivingCheck()
    {
        if (HP > 0)     //単純にダメージを受けたときの処理
        {
            //カッターの時
            if (hitSkilID == 2)
            {
                //SetCutterContactCue();
                criAtomSource.Play("CutterContract");
            }
            else if (hitSkilID == 3)//おはようブラストの時
            {
                SetContactCue();
                criAtomSource?.Play(contactSoundName);
            }
            else //それ以外
            {
                SetAttackCue();
                criAtomSource?.Play(attackSoundName);
            }

            //振動させる
            StartCoroutine(DoShake(duration, magnitude));
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
    protected void SetAttackCue()
    {
        switch (objectID)
        {
            case 0: attackSoundName = "BuildingContact00"; break;
            case 1: attackSoundName = "PoleContact00"; break;
            case 2: attackSoundName = "TrachcanContact00"; break;
            case 3: attackSoundName = "Contact_Manhole00"; break;
            case 4: attackSoundName = "BuildingContact00"; break;
            case 5: attackSoundName = "CarContact00"; break;
            case 6: attackSoundName = "BuildingContact00"; break;
            case 7: attackSoundName = "KickTree00"; break;
            case 8: attackSoundName = "FireHydrant00"; break;
            case 9: attackSoundName = "TrafficExplosion00"; break;
            case 10: attackSoundName = "BuildingContact00"; break;
            case 11: attackSoundName = "ChefContact"; break;
            case 12: attackSoundName = "PoliceContact"; break;
            default: attackSoundName = "TrachcanContact00"; break;
        }
        if (criAtomSource != null) criAtomSource.cueName = attackSoundName;
    }

    protected void SetCutterContactCue()
    {
        switch (objectID)
        {
            //後で入れます
            default: break;
        }
        if (criAtomSource != null) criAtomSource.cueName = CutterContactSoundName;
    }

    protected void SetContactCue()
    {
        switch (objectID)
        {
            case 0: contactSoundName = "BuildingContact00"; break;
            case 1: contactSoundName = "PoleContact00"; break;
            case 2: contactSoundName = "TrachcanContact00"; break;
            case 3: contactSoundName = "Contact_Manhole00"; break;
            case 4: contactSoundName = "BuildingContact00"; break;
            case 5: contactSoundName = "CarContact00"; break;
            case 6: contactSoundName = "BuildingContact00"; break;
            case 7: contactSoundName = "KickTree00"; break;
            case 8: contactSoundName = "Contact_FireHydrant00"; break;
            case 9: contactSoundName = "PoleContact00"; break;
            case 10: contactSoundName = "BuildingContact00"; break;
            case 11: contactSoundName = "ChefContact"; break;
            case 12: contactSoundName = "PoliceContact"; break;
            default: contactSoundName = "TrachcanContact00"; break;
        }
        if (criAtomSource != null) criAtomSource.cueName = contactSoundName;
    }

    protected void SetBreakCue()
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
            case 10: ExplosionSoundName = "GasStation00"; break;
            case 11: ExplosionSoundName = "ChefDie"; break;
            case 12: ExplosionSoundName = "PoliceDie"; break;
            default: ExplosionSoundName = "PoleExplosion00"; break;
        }
        if (criAtomSource != null) criAtomSource.cueName = ExplosionSoundName;
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

    //派生クラス用にメソッドを分化しました
    protected bool ChangeToDeath()
    {
        //すでに破壊済みの場合は無視
        if (!livingFlg) return false;

        livingFlg = false;
        //一応、HPは0になるように補正をかける
        HP = 0;

        GameObject.Find("Canvas").GetComponent<Parameters_R>().ScoreManager(breakScore);
        //餌のスポーン処理
        scrFood?.DropFood();

        SetBreakCue();
        //加筆　undertreem 0625
        //float BusLevel = ADX_RevLevel.ADX_BusSendLevel;
        //SetBusSendLevelSet(Rev, BusLevel);
        //Debug.Log(BusLevel);
        //カッターのときはカッターキューを鳴らす
        if (hitSkilID == 2 && objectID == 0) criAtomSource.Play("CutterBuilExplosion");
        else if (hitSkilID == 2) criAtomSource?.Play("CutterCut00");
        else criAtomSource?.Play(ExplosionSoundName);

        //全て処理しきった場合は次のDeathメソッドを実行
        return true;
    }

    protected virtual void Death()
    {
        if (!ChangeToDeath()) return;

        if (GetComponent<Car_R>() != null)
        {
            Destroy(GetComponent<Animator>());
            Destroy(GetComponent<Car_R>());
        }

        DeathCount();

        //差し替え処理  分割オブジェクトが設定されていないか技がカッターの時には無視する
        if (divideObject != null && hitSkilID != 2) ChangeObject();
        else Substitution();    //差し替えをしない場合

    }

    public void DeathCount()
    {
        missionScr = player.GetComponent<Missions_M>();
        //M 追加
        missionScr.hitID = hitSkilID;

        switch (gameObject.tag)
        {
            case "Small": missionScr.SmallNumberPlus(); break;
            case "Tree": missionScr.SmallNumberPlus(); break;
            case "Big": missionScr.BigNumberPlus(); break;
            default: break;
        }
    }

    //差し替えする場合
    private void ChangeObject()
    {
        var genPos = transform.position;
        var dividedObject = Instantiate(divideObject, genPos, transform.rotation);
        var breakScr = dividedObject.AddComponent<ObjectBreak_Y>();
        breakScr.InitSetting(this, true);
        breakScr.BreakAction();

        Delete();
        gameObject.SetActive(false);
    }
    //差し替えしない場合
    protected void Substitution()
    {
        var breakScr = gameObject.AddComponent<ObjectBreak_Y>();
        breakScr.InitSetting(this, false);
        breakScr.BreakAction();
        Delete();
    }

    public void Delete()
    {
        Destroy(gameObject, deleteTime);
    }
}