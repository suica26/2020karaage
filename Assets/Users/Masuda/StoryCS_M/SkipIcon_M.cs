using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIcon_M : MonoBehaviour
{
    [SerializeField] GameObject skip,next,canvas;
    private CriAtomSource cas;
    void Start()
    {
        skip.SetActive(false);
        cas = (CriAtomSource)GetComponent("CriAtomSource");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSkip()
    {
        skip.SetActive(true);
    }

    public void OffSkip()
    {
        skip.SetActive(false);
    }

    public void SkipStory()
    {
        next.SetActive(true);
        skip.SetActive(false);
        Destroy(canvas);
    }
}
