using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        resultScore = scrParameter.score;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    void OnEnable()
    {
        gameOverText.text = "Score: " + resultScore;
    }
}