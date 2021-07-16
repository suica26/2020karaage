using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeImpact_R : MonoBehaviour
{
    [SerializeField] private GameObject preCube = null;

    private GameObject player = null;
    private bool attackFlag = true;
    private float deleteTime = 0.5f;
    private int cubeNum = 0;

    List<GameObject> cubeList = new List<GameObject>();

    List<GameObject> objList = new List<GameObject>();

    private GameObject objCube = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        cubeNum = Random.Range(10, 21);
        for (int i = 0; i < cubeNum; i++)
        {
            objCube = preCube;
            Instantiate(objCube,gameObject.transform);
            objCube.transform.position = new Vector3(Random.Range(-3f, 3f), 1, Random.Range(-3f, 3f));
            objCube.transform.rotation = new Quaternion(Random.Range(0, 360) * Mathf.Deg2Rad, Random.Range(0, 360) * Mathf.Deg2Rad, Random.Range(0, 360) * Mathf.Deg2Rad, 1);
            objCube.transform.localScale = Vector3.one * Random.Range(0.1f, 0.3f);
            objCube.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(100, 150), ForceMode.Acceleration);
            cubeList.Add(objCube);
        }
    }

    private void Update()
    {
        deleteTime -= Time.deltaTime;
        if (attackFlag && objList != null)
        {
            foreach(var obj in objList)
            {
                obj.GetComponent<Material>().color = new Color(255, 0, 0);
            }
        }

        if (deleteTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != player || collision.gameObject.CompareTag("GameController"))
        {
            objList.Add(collision.gameObject);
        }
    }
}
