using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_SoundPlay : MonoBehaviour
{
    private new CriAtomSource audio;
    bool isCalledOnce;
    // Start is called before the first frame update
    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");

    }
    void OnEnable()
    {
            Invoke("SoundPlay0", 1.2f);
            Invoke("SoundPlay1", 2.7f);
    }

        // Update is called once per frame
        void Update()
    {
    }
    private void SoundPlay0()
    {

        audio.Play("Mission_WindowMove");
    }
    private void SoundPlay1()
    {

        audio.Play("Mission_WindowClose");
    }
}
