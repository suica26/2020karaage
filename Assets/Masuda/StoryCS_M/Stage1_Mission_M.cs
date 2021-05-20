using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1_Mission_M : MonoBehaviour
{
    [SerializeField] public Text mission;
    [SerializeField] public GameObject Player, check;
    [SerializeField] public TextAsset txtFile;
    private string txtData;
    private string[] splitText;

    void Start()
    {
        txtData = txtFile.text;
        splitText = txtData.Split(char.Parse("\n"));
        mission.text = splitText[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "jama")
        {
            Destroy(check);
            mission.text = splitText[4];
        }
    }
}
