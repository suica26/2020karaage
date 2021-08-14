using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX_CollHoneSound : MonoBehaviour
{
    private new CriAtomSource audio;
    private float currentTime = 0f;
    private float span = 0.1f;
    // Start is called before the first frame update

    Ray ray;

    void Start()
    {
        audio = (CriAtomSource)GetComponent("CriAtomSource");

    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > span)
        {
            ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.BoxCast(transform.position, Vector3.one * 0.5f, transform.forward, out hit, Quaternion.identity, 7.5f))
            {
                if (hit.transform.gameObject.name == "Player")
                {
                    audio.Play("Hone00");
                }
            }
            currentTime = 0f;
        }
    }

}
