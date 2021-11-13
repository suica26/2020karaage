using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Clear_M : MonoBehaviour
{
    [SerializeField] public GameObject clear, company, next, pause;
    public bool stageClear, scoreM;
    void Start()
    {
        clear.SetActive(false);
        next.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreAttack_Y.gameMode == mode.ScoreAttack)
        {
            scoreM = true;
        }

        if (!company && !stageClear && !scoreM)
        {
            Time.timeScale = 0;
            //Destroy(pause);
            Cursor.visible = true;
            clear.SetActive(true);
            next.SetActive(true);
            stageClear = true;
        }
    }
}
