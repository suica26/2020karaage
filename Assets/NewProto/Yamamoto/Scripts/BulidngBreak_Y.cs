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
            Destroy(objBoxCollider);　　//BoxCollider削除
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

        //振動させる
        Shake(0.25f, 0.1f);
    }

    // 吹き飛ばしメソッド
    void Explode()
    {
        foreach (GameObject obj in myParts)
        {
            obj.GetComponent<Rigidbody>().isKinematic = false;
            if (hitSkilID == 3)
            {
                GameObject cutter = GameObject.Find("Cutter(Clone)");
                var cutterDir = cutter.GetComponent<Rigidbody>().velocity.normalized;
                var objPos = obj.transform.position - cutter.transform.position;
                //XZ二次元化
                cutterDir.y = 0;
                objPos.y = 0;
                //外積計算
                var cross = Vector3.Cross(cutterDir, objPos);
                //外積で左右どちらに飛ばすか決定
                //XZ平面での計算なのでyで左右判定
                if (cross.y > 0)
                {
                    var force = new Vector3(cutterDir.z, 0, -cutterDir.x) * obj.transform.position.y/2;
                    obj.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                }
                else
                {
                    var force = new Vector3(-cutterDir.z, 0, cutterDir.x) * obj.transform.position.y/2;
                    obj.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                }
            }
            else
            {
                Vector3 forcePower = new Vector3(Random.Range(-Power, Power), Random.Range(-Power * 0.5f, Power * 0.5f), Random.Range(-Power * 0.5f, Power * 0.5f));
                Vector3 TorquePower = new Vector3(Random.Range(-Torque, Torque), Random.Range(-Torque, Torque), Random.Range(-Torque, Torque));

                obj.GetComponent<Rigidbody>().AddForce(forcePower, ForceMode.Impulse);
                obj.GetComponent<Rigidbody>().AddTorque(TorquePower, ForceMode.Impulse);
            }
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
