using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter_R : MonoBehaviour
{
    [SerializeField] private GameObject preCutter = null;

    public bool throwingCutter = false;

    GameObject cutter;

    // Start is called before the first frame update
    void Start()
    {
        throwingCutter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !throwingCutter)
        {
            throwingCutter = true;
            cutter = Instantiate(preCutter, transform.position + (transform.forward) + (transform.up * 0.5f), Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, -90)));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("HiHi");
        if(other.gameObject == cutter)
        {
            Debug.Log("Hi");
            Destroy(other.gameObject);
            throwingCutter = false;
        }
    }
}
