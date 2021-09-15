using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIcon_M : MonoBehaviour
{
    [SerializeField] GameObject skip, next;
    public MovieSkip_M movSkip;
    private CriAtomSource cas;
    void Start()
    {
        skip.SetActive(false);
        cas = (CriAtomSource)GetComponent("CriAtomSource");
    }

    public void OnSkip()
    {
        skip.SetActive(true);
    }

    public void OffSkip()
    {
        skip.SetActive(false);
        movSkip.ResetStates();
    }

    public void SkipStory()
    {
        next.SetActive(true);
        skip.SetActive(false);
    }
}
