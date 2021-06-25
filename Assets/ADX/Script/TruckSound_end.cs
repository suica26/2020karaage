using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSound_end : MonoBehaviour
{
    private new CriAtomSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");
    }

    // Update is called once per frame
    void Update()
    {

    }
    // アニメーションが終了したときに呼ばれるメソッド
    public void OnAnimationCompleted()
    {
        audio.Stop();
    }
}
