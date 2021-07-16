using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1_Mission_M : MonoBehaviour
{
    [SerializeField] public Text mission, submis, exmis, count, per, tips;
    [SerializeField] public GameObject player, shop, misBox, company, hip,
        tipsCircle, tipsChicken;
    [SerializeField] public TextAsset txtFile;
    [SerializeField] public int smallNum, bigNum, achieve;
    [SerializeField] public int smallBorder1, smallBorder2, smallBorder3,
                                bigBorder1, bigBorder2, bigBorder3,bigBorder4;
    [SerializeField] public int manhole, hydrant;
    public bool first = true, second = false, third = false, fourth = false, final = false,
        Language,hipStamp = false, tip = false;
    private string txtData;
    private string[] splitText;
    [SerializeField] Animation missionSlide;
    [SerializeField] float timer,tipsTimer;

    void Start()
    {
        txtData = txtFile.text;
        splitText = txtData.Split(char.Parse("\n"));
        misBox.SetActive(false);
        hip.SetActive(false);
        tipsCircle.SetActive(false);
        tipsChicken.SetActive(false);
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
            second = true;
            per.text = "0%";
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
            hipStamp = true;
        }

        if (fourth && hipStamp)
        {
            timer += Time.unscaledDeltaTime/2;
        }

        if (timer >= 3.0f)
        {
            Time.timeScale = 0f;
            hip.SetActive(true);
            //Cursor.visible = true;
        }

        if (timer >= 5.0f)
        {
            Time.timeScale = 1f;
            hip.SetActive(false);
            hipStamp = false;
        }

        if (!hipStamp)
        {
            //Cursor.visible = false;
            timer = 0;
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
            per.text = "";
        }

        if (final)
        {
            tipsTimer += Time.deltaTime;
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

        if (tipsTimer >= 180 && !tip)
        {
            tips.text = "アジトは金色に輝いているみたい...？？";
            tip = true;
            tipsChicken.SetActive(true);
        }

        else if (tipsTimer >= 300 && tip)
        {
            tips.text = "消火栓やマンホールを使って\n見渡してみよう...！";
            tipsCircle.SetActive(true);

        }

        if (achieve >= 99)
        {
            achieve = 0;
        }

        //後で消す
        if (Input.GetKeyDown(KeyCode.Q))
        {
            fourth = true;
            hipStamp = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            manhole += 3;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            tipsTimer += 60;
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

    /*public void OnClick()
    {
        Time.timeScale = 1f;
        hip.SetActive(false);
        hipStamp = false;
    }*/

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
