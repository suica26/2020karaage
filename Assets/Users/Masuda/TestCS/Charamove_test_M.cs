using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charamove_test_M : MonoBehaviour
{
    private Vector3 latestPos;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = transform.position - latestPos;
        latestPos = transform.position;

        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 3 * Time.deltaTime);
            mainCamera.transform.position += new Vector3(0, 0, 3 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -3 * Time.deltaTime);
            mainCamera.transform.position += new Vector3(0, 0, -3 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(3 * Time.deltaTime, 0, 0);
            mainCamera.transform.position += new Vector3(3 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-3 * Time.deltaTime, 0, 0);
            mainCamera.transform.position += new Vector3(-3 * Time.deltaTime, 0, 0);
        }
    }
}
