using UnityEngine;

public class RestartUI : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.Reset_Timer();
        ResourceManager.Instance.Reset_Resource();
        SceneLoader.Instance.Load_Scene("Main", GameState.Playing);
    }

    public void BackToTilte()
    {
        GameManager.Instance.Reset_Timer();
        ResourceManager.Instance.Reset_Resource();
        UnlockManager.Instance.Reset_Upgrade();
        SceneLoader.Instance.Load_Scene("Title", GameState.Boot);
    }
}
