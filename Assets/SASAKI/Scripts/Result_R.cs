using UnityEngine;
using UnityEngine.UI;

/*
 * -------------------------
 *
 * ||Result_R()
 * ||
 * || ※Parameters_Rがアタッチされたオブジェクトが存在するか確認してください。
 * ||   必ず子オブジェクトにUI > Textを配置し、名称をGameOverTextとしてください。
 * ||
 * || __int resultScore (獲得した総スコアを収めておく変数)
 * ||   Text gameOverText (GameOverTextのテキストコンポーネントを格納)
 * ||
 * || =OnEnable()
 * ||  =ゲームオーバーテキストを表示する。
 * ||
 *
 * -------------------------
 */
public class Result_R : MonoBehaviour
{
    private int resultScore;
    private Text gameOverText; 

    void Start()
    {
        resultScore = GameObject.Find("Parameters").GetComponent<Parameters_R>().score;
    }

    void OnEnable()
    {
        gameOverText = transform.Find("GameOverText").GetComponent<Text>();
        gameOverText.text = "DESTROYED!\nScore: " + resultScore + "\nPress \"R\" to Retry";
    }
}
