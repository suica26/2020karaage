using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingChicken_R : MonoBehaviour
{
    [SerializeField] private int memorizeFrame;
    [SerializeField] private int maxTrackingObj;
    [SerializeField] private GameObject preObj;
    private List<Vector3> positionList;
    private List<Quaternion> rotationList;
    private List<GameObject> trackList;
    void Start()
    {
        Application.targetFrameRate = 60;
        positionList = new List<Vector3>();
        rotationList = new List<Quaternion>();
        trackList = new List<GameObject>();
    }

    void Update()
    {
        TransformListUpdate();

        int count = 0;
        if(trackList != null)
        {
            foreach (GameObject obj in trackList)
            {
                obj.transform.position = positionList[(count + 1) * memorizeFrame / maxTrackingObj - 1];
                obj.transform.rotation = rotationList[(count + 1) * memorizeFrame / maxTrackingObj - 1];
                count++;
            }
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            AddObject(preObj);
        }
    }

    private void TransformListUpdate()  //移動情報の更新
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        if(positionList.Count >= memorizeFrame)
        {
            positionList.RemoveAt(memorizeFrame - 1);
        }
        positionList.Insert(0, pos);
        rotationList.Insert(0, rot);
    }

    public void AddObject(GameObject addObj)
    {
        if(trackList == null || trackList.Count < maxTrackingObj)
        {
            var obj = Instantiate(addObj);
            trackList.Add(obj);
        }
    }
}
