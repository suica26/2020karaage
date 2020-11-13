using UnityEngine;
using UnityEngine.UI;

/*
 * -------------------------
 * 
 * ||Parameters_R()
 * || ※Canvas内にCreateEmptyを生成しParametersという名前にしてアタッチしてください。
 * ||   必ず子オブジェクトにUI > Textを4つ配置し、名称をScore・Time・EP・HPとしてください。
 * ||
 * || _int score, time, ep, hp (各パラメーターの変数)
 * ||　
 * || __bool freeze (パラメーターを変化できるようにするか否か)
 * ||   float count(タイマー制御用)
 * ||   Text scoreText, timeText, epText, hpText(各UIテキストを変更するためのオブジェクト)
 * ||   GameObject resultPanel(リザルトパネルのオブジェクト)
 * ||
 * || =ScoreManager(int)
 * ||  =引数で指定した分スコアを加算します。
 * ||
 * || =TimeManager(int)
 * ||  =引数で指定した分タイムを加算します。
 * ||
 * || =EPManager(int)
 * ||  =引数で指定した分EPを加算します。
 * ||
 * || =HPManager(int)
 * ||  =引数で指定した分HPを加算します。
 * ||
 * || ==Update()
 * ||   =時間計測を行いTimeManagerで時間を減算しています。
 * ||
 *
 * -------------------------
 */

public class Parameters_R : MonoBehaviour
{
    public int score, time, ep, hp;

    private bool freeze = false;
    private float count;
    private Text scoreText, timeText, epText, hpText;
    private GameObject resultPanel;

    void Start()
    {
        scoreText = transform.Find("Score").GetComponent<Text>();
        timeText = transform.Find("Time").GetComponent<Text>();
        epText = transform.Find("EP").GetComponent<Text>();
        hpText = transform.Find("HP").GetComponent<Text>();
        resultPanel = GameObject.Find("Canvas").transform.Find("ResultPanel").gameObject;


        scoreText.text = "Score: " + score;
        timeText.text = "Time: " + time;
        epText.text = "EP: " + ep;
        hpText.text = "HP: " + hp;

        count = time;
    }

    void ScoreManager(int addScore)
    {
        if (!freeze)
        {
            score += addScore;
            scoreText.text = "Score: " + score;
        }
    }
    //引数で指定した分だけスコアを加算します。

    void TimeManager(int addTime)
    {
        if (!freeze)
        {
            time += addTime;
            count = time;
            timeText.text = "Time: " + time;
            if (time <= 0)
            {
                freeze = true;
                resultPanel.SetActive(true);
            }
        }
    }
    //引数で指定した分だけ残りタイムを加算します。

    void EPManager(int addEP)
    {
        if (!freeze)
        {
            ep += addEP;
            epText.text = "EP: " + ep;
        }
    }
    //引数で指定した分だけEPを加算します。

    void HPManager(int addHP)
    {
        if (!freeze)
        {
            hp -= addHP;
            if (hp <= 0)
            {
                freeze = true;
                resultPanel.SetActive(true);
                hp = 0;
            }
            hpText.text = "HP: " + hp;
        }
    }
    //引数で指定した分だけHPを加算します。

    private void Update()
    {
        count -= Time.deltaTime;
        if(time - count > 1)
        {
            TimeManager(-1);
        }
    }
    //タイマーです。一秒ごとにTimeManager()で一秒減らしてます。
}
