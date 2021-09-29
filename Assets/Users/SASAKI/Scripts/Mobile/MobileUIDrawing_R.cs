using UnityEngine;

public class MobileUIDrawing_R : MonoBehaviour
{
    private bool mobileMode;
    // Start is called before the first frame update
    void Start()
    {
        mobileMode = SaveManager_Y.GetInstance().isMobile;

        if (!mobileMode)
            gameObject.SetActive(false);
    }
}
