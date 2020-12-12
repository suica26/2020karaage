using UnityEngine;
using UnityEngine.UI;

/*
 * -------------------------
 *
 * ||Result_R()
 * ||
 * || =OnEnable()
 * ||  =ゲームオーバーテキストを表示する。
 * ||
 *
 * -------------------------
 */
public class Result_R : MonoBehaviour
{
    [SerializeField] private Parameters_R scrParameter = null;
    [SerializeField] private Text gameOverText;
    private int resultScore;

    void Start()
    {
        resultScore = scrParameter.score;
    }

    void OnEnable()
    {
        gameOverText.text = "Score: " + resultScore;
    }
}