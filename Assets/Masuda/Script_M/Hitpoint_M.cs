using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hitpoint_M : MonoBehaviour
{
    int firstHP = 50;
    int currentHP;

    [SerializeField] public Slider hpSlider;
    [SerializeField] public GameObject Bullet_Y;

    void Start()
    {
        //スライダーの大きさと最初のHPを50に
        hpSlider.value = firstHP;
        hpSlider.maxValue = firstHP;
        currentHP = firstHP;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == Bullet_Y)
        {
            //砲弾に当たったらダメージ分、体力が減る
            int bulletDamage = 10;
            currentHP = currentHP - bulletDamage;
            hpSlider.value = currentHP;
        }
    }

    void FixedUpdate()
    {
        if (currentHP == 0)
        {
            //ゲームオーバー画面へ
            SceneManager.LoadScene("Result2_M");
        }
    }
}