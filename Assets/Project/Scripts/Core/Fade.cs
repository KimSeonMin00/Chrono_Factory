using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Fade : Singleton<Fade>
{
    public Image m_FadeImage;

    public Color m_FadeColor;
    public float fadeDuration = 1.0f;
    private void Start()
    {    
        m_FadeImage = GetComponent<Image>();
        this.gameObject.SetActive(false);
    }

    public void FadeTo(string sceneName, GameState state, Color Color)
    {
        this.gameObject.SetActive(true);
        m_FadeColor = Color;
        m_FadeColor.a = 0f;
        StartCoroutine(FadeOutAndLoad(sceneName, state));
    }

    IEnumerator FadeOutAndLoad(string sceneName, GameState state)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            m_FadeColor.a = Mathf.Clamp01(elapsed / fadeDuration);
            m_FadeImage.color = m_FadeColor;
            yield return null;
        }

        SceneLoader.Instance.Load_Scene(sceneName, state);

        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            m_FadeColor.a = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            m_FadeImage.color = m_FadeColor;
            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}
