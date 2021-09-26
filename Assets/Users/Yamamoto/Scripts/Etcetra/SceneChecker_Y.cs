using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChecker_Y : MonoBehaviour
{
    public int stageNum;
    void Start()
    {
        var saveObj = GameObject.Find("SaveManager");
        if (saveObj != null) saveObj.GetComponent<SaveManager_Y>().SaveStageFlg(stageNum);
        Debug.Log("Saved");
    }
}
