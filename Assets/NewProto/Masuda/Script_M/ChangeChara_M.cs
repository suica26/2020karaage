using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChara_M : MonoBehaviour
{
    [SerializeField] private Parameters_R scrEP = null;
    [SerializeField] private GameObject[] _chickenForm_tbl = null;
    [SerializeField] private int firstEvo;
    [SerializeField] private int secondEvo;
    [SerializeField] private int thirdEvo;

    private GameObject currentChickenForm = null;
    private int esaPoint;

    void Start()
    {
        _chickenForm_tbl[0].SetActive(true);
        currentChickenForm = _chickenForm_tbl[0];
    }

    void Update()
    {
        esaPoint = scrEP.ep;

        //第1形態から第2形態に変化
        if (esaPoint >= firstEvo && esaPoint < secondEvo)
        {
            currentChickenForm.SetActive(false);
            currentChickenForm = _chickenForm_tbl[1];      
        }
        //第2形態から第3形態に変化
        else if (esaPoint >= secondEvo && esaPoint < thirdEvo)
        {
            currentChickenForm.SetActive(false);
            currentChickenForm = _chickenForm_tbl[2];
        }
        //第3形態から最終形態に変化
        if (esaPoint >= thirdEvo)
        {
            currentChickenForm.SetActive(false);
            currentChickenForm = _chickenForm_tbl[3];
        }

        currentChickenForm.SetActive(true);
    }
}
