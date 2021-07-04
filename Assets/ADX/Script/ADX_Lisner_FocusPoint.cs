using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_Lisner_FocusPoint : MonoBehaviour
{
    public GameObject ADX_Lisner ;
    private GameObject MainCamera;
    private CriAtomListener A;
    private CriAtomEx3dListener Listener;
    [Header("0.0fがリスナー位置、1.0fが注目点と同じ位置")]
    public float directionFocusLevel, distanceFocusLevel;

    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");

        A = ADX_Lisner.GetComponent<CriAtomListener>();

    }

    void Update()
    {
        Listener = A.nativeListener;
        Vector3 position = MainCamera.transform.position;

        //注目点を設定（カメラを注目点）
        Listener.SetFocusPoint(position.x, position.y, position.z);

        //距離減衰計算の基準
        Listener.SetDistanceFocusLevel(distanceFocusLevel);
        //定位計算の基準
        Listener.SetDirectionFocusLevel(directionFocusLevel);

        //３Dリスナーの更新
        Listener.Update();
    }

}