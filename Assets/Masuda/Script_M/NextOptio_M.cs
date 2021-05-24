using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextOptio_M : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel1, optionPanel2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        //optionPanel1.SetActive(false);
        optionPanel2.SetActive(true);
    }
}
