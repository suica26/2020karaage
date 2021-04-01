using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshtest_M : MonoBehaviour
{
    [SerializeField] private GameObject score;
    [SerializeField] private int price;
    string strSCORE;

    void Start()
    {
        strSCORE = price.ToString();
        score.GetComponent<TextMesh>().text = "+$ " + strSCORE;
    }

    void Update()
    {
        score.transform.rotation = Camera.main.transform.rotation;
    }
}
