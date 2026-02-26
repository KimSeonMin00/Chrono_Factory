using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public void Load_Scene(string sceneName, GameState state)
    {
        SoundManager.Instance.StopAllSound();
        StartCoroutine(Load_Scene_Async(sceneName, state));
    }

    private IEnumerator Load_Scene_Async(string sceneName, GameState state)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Change_State(GameState.Loading);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            //operation.progress로 진행도 체크 가능

            yield return null;
        }

        if(GameManager.Instance != null)
            GameManager.Instance.Change_State(state);

    }
}
