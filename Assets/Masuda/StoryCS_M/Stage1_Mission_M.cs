using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1_Mission_M : MonoBehaviour
{
    [SerializeField] public Text mission, submis;
    [SerializeField] public GameObject player, shop, misBox, company;
    [SerializeField] public TextAsset txtFile;
    [SerializeField] public int breakNum, clearNum;
    private bool check = true;
    private string txtData;
    private string[] splitText;
    [SerializeField] Animation missionSlide;

    void Start()
    {
        txtData = txtFile.text;
        splitText = txtData.Split(char.Parse("\n"));
        misBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 comPos = company.transform.position;
        float dis = Vector3.Distance(playerPos, comPos);

        //本当はshopが消えたら発動する、!shopに直す
        if (Input.GetKeyDown(KeyCode.Q))
        {
            misBox.SetActive(true);
            missionSlide.Play();
            mission.text = splitText[0];
            submis.text = splitText[1];
        }

        if (breakNum == clearNum)
        {
            missionSlide.Play();
            mission.text = splitText[2];
            submis.text = splitText[3];
        }

        //これはテスト用、本番は消す
        if (Input.GetKeyDown(KeyCode.P) && check == true)
        {
            missionSlide.Play();
            mission.text = splitText[4];
            submis.text = splitText[5];
            check = false;
        }

        if (dis <= 30 && check)
        {
            missionSlide.Play();
            mission.text = splitText[4];
            submis.text = splitText[5];
            check = false;
        }
    }

    /*public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "jama")
        {
            Destroy(check);
            missionSlide.Play();
            mission.text = splitText[4];
            submis.text = splitText[5];
        }
    }*/
}
