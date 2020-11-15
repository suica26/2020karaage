using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMove_M : MonoBehaviour
{
    private CharacterController charaCon;    //CharacterController型の変数charaConを用意します
    private Vector3 playerVelocity;                 //playerの速度を格納するVector3型の変数を用意します
    private bool groundedPlayer;                    //bool型の変数groundedPlayerを用意します（地面に着いてるかどうか？を判断）
    public float playerSpeed = 5.0f;                //Playerの移動スピードの変数をpublicにしてインスペクターから変えられるようにします
    public float jumpHeight = 2.0f;                 //Playerのジャンプ力の変数をpublicにしてインスペクターから変えられるようにします
    private float gravity = -9.81f;                     //落下する力を入れる変数を用意して、下方向に「9.81」とします
    public float rotSpeed = 700f;                    //落下する力を入れる変数をpublicにして、スピード値を入れます


    // Start is called before the first frame update
    void Start()
    {
        charaCon = gameObject.GetComponent<CharacterController>();　//変数charaConにCharacterController コンポネントを入れます
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = charaCon.isGrounded;         //フラグの変数groundedPlayerにCharacterControllerのboolフラグisGroundedを入れます
        if (groundedPlayer && playerVelocity.y < 0)     //接地していて、且つプレイヤーのy方向が下向き（落ちて）だったら
        {
            playerVelocity.y = 0f;                // プレイヤーが落ちていくのを止めます（接地してるのに落ちたら、地面を突き抜けてしまう）
        }

        //Vector3型の変数dirをつくり、”左右”と”上下”キーの入力を入れます
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //CharacterControllerのMove()関数、入れられたVecto3の方向にオブジェクトを移動させます
        charaCon.Move(dir * Time.deltaTime * playerSpeed);

        if (dir != Vector3.zero)                                       //もし変数dirの値がゼロでなければ（入力があれば・・）
        {
            Quaternion qua = Quaternion.LookRotation(dir);          // 向きたい方角をQuaternion型に直す
                                                                    // オブジェクトの向きを 変数qua に向けて、じわじわと補完しながらrotSpeedの大きさに応じて回転させる
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, rotSpeed * Time.deltaTime);
        }
        // プレイヤーのジャンプの計算
        if (Input.GetButtonDown("Jump") && groundedPlayer)      //もしスペースキーが押され、且つプレイヤーが接地していたら
        {
            //プレイヤーのy軸方向の値に「jumpHeight変数の値×重力の大きさ（反対向き、＋方向）」を足します
            playerVelocity.y += jumpHeight * -1.0f * gravity;
        }

        //プレイヤーのy軸方向の速度に重力（－の値）×フレーム間の時間経過を足します（常に下方向に向かわせている）
        playerVelocity.y += gravity * Time.deltaTime;
        //charactercontollerプレイヤーのMove()関数、Vector3にy方向の速度成分を加えたので、下方向に移動するようになります
        charaCon.Move(playerVelocity * Time.deltaTime);

    }
}