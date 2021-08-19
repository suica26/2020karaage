using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Animationから発音させるメソッド用スクリプト
public class ADX_SoundFromAnim : MonoBehaviour
{
    public new CriAtomSource audio;

    public void PlaySE(string cueName)
    {
        audio.Play(cueName);
    }
}
