using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1_Mission_M : MonoBehaviour
{
    [SerializeField] public Text mission, submis, exmis, count;
    [SerializeField] public GameObject player, shop, misBox, company;
    [SerializeField] public TextAsset txtFile;
    [SerializeField] public int smallNum, bigNum;
    [SerializeField] public int smallBorder1, smallBorder2, smallBorder3,
                                bigBorder1, bigBorder2, bigBorder3,bigBorder4;
    private bool check = false, border = false, first = true, Language;
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

        if (!shop && first)
        {
            misBox.SetActive(true);
            missionSlide.Play();
            mission.text = splitText[0];
            submis.text = splitText[1];
            exmis.text = splitText[2];
            count.text = "1";
            first = false;
            border = true;
        }

        if (bigNum >= bigBorder4 && border == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            border = false;
            check = true;
        }
        else if (bigNum >= bigBorder3 && smallNum >= smallBorder1 && border == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            border = false;
            check = true;
        }
        else if (bigNum >= bigBorder2 && smallNum >= smallBorder2 && border == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            border = false;
            check = true;
        }
        else if (bigNum >= bigBorder1 && smallNum >= smallBorder3 && border == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            border = false;
            check = true;
        }

        if (dis <= 30 && check)
        {
            missionSlide.Play();
            mission.text = splitText[6];
            submis.text = splitText[7];
            exmis.text = splitText[8];
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
