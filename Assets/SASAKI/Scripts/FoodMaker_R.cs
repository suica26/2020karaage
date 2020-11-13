using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * -------------------------
 *
 * ||FoodMaker_R()
 * ||
 * || ※objに、このスクリプトをアタッチしたオブジェクトは置かないでください。無限増殖します。
 * ||   現状ではオブジェクトを削除するようにしているのでCreateEmptyにアタッチしてください。
 * ||
 * || _int amountOfFood (設置するオブジェクトの数)
 * ||  float mapSize (ランダム生成の範囲を指定します)
 * ||  GameObject obj (設置したいオブジェクトを格納する)
 * ||
 * || __float x,z
 * ||
 * || =Start()
 * ||  =ランダムにオブジェクトを設置します。
 * ||
 *
 * -------------------------
 */
public class FoodMaker_R : MonoBehaviour
{
    public int amountOfFood;
    public float mapSize;
    float x, z;

    public GameObject obj;
    void Start()
    {
        for(int i = 0; i < amountOfFood; i++)
        {
            x = Random.Range(-mapSize, mapSize);
            z = Random.Range(-mapSize, mapSize);

            Instantiate(obj, new Vector3(x, 0.5f, z), Quaternion.identity);
        }
        Destroy(this);
    }
}
