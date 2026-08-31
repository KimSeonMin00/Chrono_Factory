using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

//Scene 전환시 페이드 인/아웃 효과를 주는 스크립트
public class Fade : Singleton<Fade>
{
    public Image m_FadeImage;

    public Color m_fadeColor;
    public float m_fFadeDuration = 1.0f;
    private void Start()
    {    
        m_FadeImage = GetComponent<Image>();
        this.gameObject.SetActive(false);
    }

    public void FadeTo(string sceneName, GameState state, Color Color)
    {
        this.gameObject.SetActive(true);
        m_fadeColor = Color;
        m_fadeColor.a = 0f;
        StartCoroutine(FadeOutAndLoad(sceneName, state));
    }

    IEnumerator FadeOutAndLoad(string sceneName, GameState state)
    {
        float elapsed = 0f;
        while (elapsed < m_fFadeDuration)
        {
            elapsed += Time.deltaTime;
            m_fadeColor.a = Mathf.Clamp01(elapsed / m_fFadeDuration);
            m_FadeImage.color = m_fadeColor;
            yield return null;
        }

        SceneLoader.Instance.Load_Scene(sceneName, state);

        elapsed = 0f;
        while (elapsed < m_fFadeDuration)
        {
            elapsed += Time.deltaTime;
            m_fadeColor.a = Mathf.Clamp01(1f - (elapsed / m_fFadeDuration));
            m_FadeImage.color = m_fadeColor;
            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}
