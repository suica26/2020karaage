using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1_Mission_M : MonoBehaviour
{
    [SerializeField] public Text mission, submis,count;
    [SerializeField] public GameObject player, shop, misBox, company;
    [SerializeField] public TextAsset txtFile;
    [SerializeField] public int smallNum, bigNum;
    [SerializeField] public int smallBorder1, smallBorder2, smallBorder3,
                                bigBorder1, bigBorder2, bigBorder3;
    private bool check = false, border = false, Language;
    private string txtData;
    private string[] splitText;
    [SerializeField] Animation missionSlide;

    void Start()
    {
        txtData = txtFile.text;
        splitText = txtData.Split(char.Parse("\n"));
        misBox.SetActive(false);
    }

    void Update()
    {
        Language = ChangeLanguage.getLanguage();//
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
            count.text = "1";
            border = true;
        }

        if (bigNum >= bigBorder3 && border == true)
        {
            missionSlide.Play();
            mission.text = splitText[2];
            submis.text = splitText[3];
            count.text = "2";
            border = false;
            check = true;
        }
        else if (bigNum >= bigBorder2 && smallNum >= smallBorder1 && border == true)
        {
            missionSlide.Play();
            mission.text = splitText[2];
            submis.text = splitText[3];
            count.text = "2";
            border = false;
            check = true;
        }
        else if (bigNum >= bigBorder1 && smallNum >= smallBorder2 && border == true)
        {
            missionSlide.Play();
            mission.text = splitText[2];
            submis.text = splitText[3];
            count.text = "2";
            border = false;
            check = true;
        }
        else if (smallNum >= smallBorder3 && border == true)
        {
            missionSlide.Play();
            mission.text = splitText[2];
            submis.text = splitText[3];
            count.text = "2";
            border = false;
            check = true;
        }

        //これはテスト用、本番は消す
        if (Input.GetKeyDown(KeyCode.P) && check == true)
        {
            missionSlide.Play();
            mission.text = splitText[4];
            submis.text = splitText[5];
            count.text = "3";
            check = false;
        }

        if (dis <= 30 && check)
        {
            missionSlide.Play();
            mission.text = splitText[4];
            submis.text = splitText[5];
            count.text = "3";
            check = false;
        }
    }

    public void BigNumberPlus()
    {
        bigNum ++;
    }
    public void SmallNumberPlus()
    {
        smallNum ++;
    }

    public void Japanese()
    {
       
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
