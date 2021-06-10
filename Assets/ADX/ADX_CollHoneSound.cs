using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_CollHoneSound : MonoBehaviour
{
    private new CriAtomSource audio;

    // Start is called before the first frame update

    Ray ray;

    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
        
    }
    // Update is called once per frame
    void Update()
    {

        //レイの生成(レイの原点、レイの飛ぶ方向)
        ray = new Ray(transform.position, transform.forward);
        //レイの判定(飛ばすレイ、レイが当たったものの情報、レイの長さ)
        if (Physics.Raycast(ray, out RaycastHit hit, 7.5f))
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.name == "Player")
            {
                audio.Play("Hone00");
            }
        }
        //Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.red);
    }

}
