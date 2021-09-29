using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileSetting_R : MonoBehaviour
{
    // ここを書き換えるとモバイルと通常版の操作を切り替えられます
    private bool mobileMode = false;

    private static MobileSetting_R instance = new MobileSetting_R();

    public static MobileSetting_R GetInstance()
    {
        return instance;
    }

    private MobileSetting_R()
    {
        mobileMode = SaveManager_Y.GetInstance().isMobile;
    }

    public bool IsMobileMode()
    {
        return mobileMode;
    }
}
