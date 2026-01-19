using UnityEngine;

public class RestartUI : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.Reset_Timer();
        ResourceManager.Instance.Reset_Resource();
        SceneLoader.Instance.Load_Scene("Main");
    }
}
