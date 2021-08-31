using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ADX_Lisner_FocusPoint : MonoBehaviour
{
    private GameObject MainCamera, player;
    private CriAtomListener LisnerA;
    private CriAtomEx3dListener Listener;
    [Header("0.0fがリスナー位置、1.0fが注目点と同じ位置")]
    private float directionFocusLevel;
    private float distanceFocusLevel;
    public GameObject PanPosObject, VolPosObject, CamPosObject;
    public bool DebugMode;
    private EvolutionChicken_R scrEvo;
    private float m, n;
    //各形態の中心Y座標
    private float CharaCenter;

    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");

        LisnerA = this.GetComponent<CriAtomListener>();
        scrEvo = player.GetComponent<EvolutionChicken_R>();

        //Camera:Player = m:nに内分
        m = 9.9f; n = 0.1f;
    }

    void Update()
    {
        EvoNumCheck();
        Listener = LisnerA.nativeListener;
        Vector3 Cameraposition = MainCamera.transform.position;

        //注目点を設定（カメラを注目点）
        Listener.SetFocusPoint(Cameraposition.x, Cameraposition.y, Cameraposition.z);

        //距離減衰計算の基準
        Listener.SetDistanceFocusLevel(distanceFocusLevel);
        //定位計算の基準
        Listener.SetDirectionFocusLevel(directionFocusLevel);

        //３Dリスナーの更新
        Listener.Update();

        //this.位置設定
        Vector3 ThisPos = (n * Cameraposition + m * (player.transform.position + new Vector3(0.0f, CharaCenter, 0.0f))) / (m + n);
        this.transform.position = ThisPos;

        //デバッグ用可視化点生成・移動・削除
        if (DebugMode == true && PanPosObject != null && VolPosObject != null & CamPosObject != null)
        {
            GameObject PanObject = Instantiate(PanPosObject, new Vector3(0, -10, 0), Quaternion.identity);
            GameObject VolObject = Instantiate(VolPosObject, new Vector3(0, -10, 0), Quaternion.identity);
            GameObject CamObject = Instantiate(CamPosObject, new Vector3(0, -10, 0), Quaternion.identity);
            Vector3 PanPos = this.transform.position + ((Cameraposition - this.transform.position) * directionFocusLevel);
            Vector3 VolPos = this.transform.position + ((Cameraposition - this.transform.position) * distanceFocusLevel);
            PanObject.transform.position = PanPos;
            VolObject.transform.position = VolPos;
            CamObject.transform.position = Cameraposition;
            StartCoroutine(DelayMethod(1, () => { Destroy(PanObject); Destroy(VolObject); Destroy(CamObject); }));

        }
    }

    private IEnumerator DelayMethod(int delayFrameCount, Action action)
    {
        for (var i = 0; i < delayFrameCount; i++)
        {
            yield return null;
        }
        action();
    }

    //各形態のフォーカスポイント位置設定
    private void EvoNumCheck()
    {
        if (scrEvo.EvolutionNum == 0)
        {
            CharaCenter = 0.4f;
            directionFocusLevel = 0.5f;
            distanceFocusLevel = 0.0f;
        }
        else if(scrEvo.EvolutionNum == 1)
        {
            CharaCenter = 2.0f;
            directionFocusLevel = 0.5f;
            distanceFocusLevel = 0.1f;
        }
        else if(scrEvo.EvolutionNum == 2)
        {
            CharaCenter = 5.0f;
            directionFocusLevel = 0.7f;
            distanceFocusLevel = 0.2f;
        }
        else if(scrEvo.EvolutionNum == 3)
        {
            CharaCenter = 12.0f;
            directionFocusLevel = 0.8f;
            distanceFocusLevel = 0.3f;
        }
        else { Debug.Log("EvoNumCheck is Errer"); }
    }
}