using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter_R : MonoBehaviour
{
    [SerializeField] private GameObject preCutter = null;
    [SerializeField] private float intervalTime;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.J) && timer <= 0)
        {
            timer = intervalTime;
            Instantiate(preCutter, transform.position, Quaternion.Euler(0, 0, -30));
        }
    }
}
