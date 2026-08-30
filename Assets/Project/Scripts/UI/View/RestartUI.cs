using UnityEngine;

public class RestartUI : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.Reset_Timer();
        ResourceManager.Instance.Reset_Resource();
        SaveManager.Instance.Save();
        SceneLoader.Instance.Load_Scene("Main", GameState.Playing);
    }

    public void BackToTilte()
    {
        GameManager.Instance.Reset_Timer();
        ResourceManager.Instance.Reset_Resource();
        UpgradeManager.Instance.Reset_Upgrade();
        SceneLoader.Instance.Load_Scene("Title", GameState.Boot);
    }

    public void Load_Game()
    {
        GameManager.Instance.Reset_Timer();
        ResourceManager.Instance.Reset_Resource();
        UpgradeManager.Instance.Reset_Upgrade();

        SaveManager.Instance.Load();
        SceneLoader.Instance.Load_Scene("Main", GameState.Playing);
    }
}
