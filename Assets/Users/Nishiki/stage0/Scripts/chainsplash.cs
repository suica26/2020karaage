using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainsplash : MonoBehaviour
{

    // 自身の子要素を管理するリスト
    List<GameObject> myParts = new List<GameObject>();

    public doorscore door;


    // Start is called before the first frame update
    void Start()
    {

        // 自分の子要素をチェック
        foreach (Transform child in gameObject.transform)
        {

            // ビルパーツに Rigidbody2D を追加して Kinematic にしておく
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // 子要素リストにパーツを追加
            myParts.Add(child.gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (door.nowanim >= 5)
        {
            Invoke("ExplodeM", 0.3f);
        }
    }


    void ExplodeM()
    {

        // 各パーツをふっとばす
        foreach (GameObject obj in myParts)
        {

            // 飛ばすパワーと回転をランダムに設定
            Vector3 forcePower = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3));



            // パーツをふっとばす！
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<Rigidbody>().AddForce(forcePower, ForceMode.Impulse);


        }
    }

}
