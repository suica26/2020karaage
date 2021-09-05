using UnityEngine;

public class MobileUIDrawing_R : MonoBehaviour
{
    private bool mobileMode;
    // Start is called before the first frame update
    void Start()
    {
        mobileMode = MobileSetting_R.GetInstance().IsMobileMode();
        if (!mobileMode)
            gameObject.SetActive(false);
    }
}
