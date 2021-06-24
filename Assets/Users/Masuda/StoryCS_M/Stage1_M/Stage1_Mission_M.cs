using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1_Mission_M : MonoBehaviour
{
    [SerializeField] public Text mission, submis, exmis, count, per;
    [SerializeField] public GameObject player, shop, misBox, company,achievement;
    [SerializeField] public TextAsset txtFile;
    [SerializeField] public int smallNum, bigNum, achieve;
    [SerializeField] public int smallBorder1, smallBorder2, smallBorder3,
                                bigBorder1, bigBorder2, bigBorder3,bigBorder4;
    [SerializeField] public int manhole, hydrant;
    public bool first = true, second = false, third = false, fourth = false, final = false,
        Language;
    private string txtData;
    private string[] splitText;
    [SerializeField] Animation missionSlide;

    void Start()
    {
        txtData = txtFile.text;
        splitText = txtData.Split(char.Parse("\n"));
        misBox.SetActive(false);
        achievement.SetActive(false);
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
            achievement.SetActive(true);
            missionSlide.Play();
            mission.text = splitText[0];
            submis.text = splitText[1];
            exmis.text = splitText[2];
            count.text = "1";
            first = false;
            second = true;
        }

        if (bigNum >= bigBorder4 && second == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            second = false;
            third = true;
            achieve = 0;
            per.text = achieve + "/ 3";
        }
        else if (bigNum >= bigBorder3 && smallNum >= smallBorder1 && second == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            second = false;
            third = true;
            achieve = 0;
            per.text = achieve + "/ 3";
        }
        else if (bigNum >= bigBorder2 && smallNum >= smallBorder2 && second == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            second = false;
            third = true;
            achieve = 0;
            per.text = achieve + "/ 3";
        }
        else if (bigNum >= bigBorder1 && smallNum >= smallBorder3 && second == true)
        {
            missionSlide.Play();
            mission.text = splitText[3];
            submis.text = splitText[4];
            exmis.text = splitText[5];
            count.text = "2";
            second = false;
            third = true;
            achieve = 0;
            per.text = achieve + "/ 3";
        }

        if (hydrant >= 3 && third)
        {
            third = false;
            fourth = true;
            missionSlide.Play();
            mission.text = splitText[6];
            submis.text = splitText[7];
            exmis.text = splitText[8];
            count.text = "3";
            achieve = 0;
            per.text = achieve + "/ 3";
        }

        if (manhole >= 3 && fourth)
        {
            fourth = false;
            final = true;
            missionSlide.Play();
            mission.text = splitText[9];
            submis.text = splitText[10];
            exmis.text = splitText[11];
            count.text = "4";
            achievement.SetActive(false);
        }

        if (dis <= 30 && final)
        {
            missionSlide.Play();
            mission.text = splitText[12];
            submis.text = splitText[13];
            exmis.text = splitText[14];
            count.text = "5";
            final = false;
        }

        if (achieve >= 99)
        {
            achieve = 0;
        }
    }

    public void BigNumberPlus()
    {
        bigNum ++;
        if (second)
        {
            achieve += 20;
            per.text = achieve + "%";
        }
    }
    public void SmallNumberPlus()
    {
        smallNum ++;
        if (second)
        {
            achieve += 4;
            per.text = achieve + "%";
        }
    }

    public void Manhole()
    {

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
