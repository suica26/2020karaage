using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadingstage : MonoBehaviour
{
    public SceneReference m_scene;

    private void Update()
    {
        
        // シーン遷移
        SceneManager.LoadScene(m_scene);
        
    }
}