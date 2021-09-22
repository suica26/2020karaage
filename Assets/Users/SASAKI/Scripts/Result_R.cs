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
    [SerializeField] private GameObject nextPanel;

    private string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        nextPanel.SetActive(false);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            nextPanel.SetActive(true);
            SceneManager.LoadScene(sceneName);
        }*/
    }

    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void OnClick()
    {
        Time.timeScale = 1f;
        nextPanel.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}