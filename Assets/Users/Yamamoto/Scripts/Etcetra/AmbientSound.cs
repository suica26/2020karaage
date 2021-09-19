using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    [SerializeField] private GameObject[] soundObjs;
    [SerializeField] private CriAtomSource[] cass;
    private GameObject player;
    [SerializeField] private float distance = 20f;
    [SerializeField] private string cueName;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        soundObjs = new GameObject[transform.childCount];
        cass = new CriAtomSource[transform.childCount];
        StartCoroutine(StartSettings());
    }

    private IEnumerator StartSettings()
    {
        yield return StartCoroutine(GetTrees());
        yield return StartCoroutine(GetCris());
        StartCoroutine(CheckDistances());
    }

    private IEnumerator GetTrees()
    {
        int count = 0;
        foreach (Transform tree in transform)
        {
            soundObjs[count] = tree.gameObject;
            count++;
            if (count % 100 == 0) yield return null;
        }
    }

    private IEnumerator GetCris()
    {
        int count = 0;
        foreach (var tree in soundObjs)
        {
            var cri = tree.GetComponent<CriAtomSource>();
            if (cri != null) cass[count] = cri;
            else cass[count] = GetComponentInChildren<CriAtomSource>();

            count++;
            if (count % 30 == 0) yield return null;
        }
    }

    private IEnumerator CheckDistances()
    {
        Debug.Log("Tree Sound Loop Start!");
        for (int i = 0; i < soundObjs.Length; i++)
        {
            if (soundObjs[i] != null)
            {
                if (Vector3.Distance(soundObjs[i].transform.position, player.transform.position) <= distance)
                {
                    PlayAndStopSound(cass[i]);
                }
                else cass[i].Stop();
            }
            if (i % 30 == 0) yield return null;
        }
        Debug.Log("Tree Sound Loop Finish!");

        RestartLoop();
    }

    //再生状況監視
    private void PlayAndStopSound(CriAtomSource cri)
    {
        if (cri != null)
        {
            if ((cri.status == CriAtomSource.Status.Stop) || (cri.status == CriAtomSource.Status.PlayEnd))
            {
                cri.Play(cueName);
            }
        }
    }

    private void RestartLoop()
    {
        StartCoroutine(CheckDistances());
    }
}
