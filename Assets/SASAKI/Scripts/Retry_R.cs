using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * -------------------------
 *
 * ||Retry_R()
 * ||
 * || ※特筆事項なし
 * ||
 * || _string sceneName (現在のシーンの名称を格納する変数)
 * ||
 * ||
 * || =Update()
 * ||  =Rキーを押すとシーンをロードします。
 * ||
 * ||
 *
 * -------------------------
 */
public class Retry_R : MonoBehaviour
{
    string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}