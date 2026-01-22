using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour
{
    public string m_SceneName;
    
    public void Scene_Load()
    {
        SceneManager.LoadScene(m_SceneName);
    }
}
