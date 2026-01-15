using UnityEngine;

public class RestartUI : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.Reset_Timer();
        SceneLoader.Instance.Load_Scene("Main");
    }
}
