using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] TooltipUI m_tootipUI;
    [SerializeField] TimerUI m_timerUI;

    [Header("GameMode")]
    [SerializeField] GameObject m_goPlaying;//게임플레이 중에만 활성화되는 UI

    public void Start()
    {
        GameManager.Instance.OnStateChanged += OnStateChanged;
    }

    public void OnStateChanged(GameState state)
    {
        if (state == GameState.Playing)
            m_goPlaying.SetActive(true);

        else
            m_goPlaying.SetActive(false);
    }

    public void Reset_Timer()
    {
        m_timerUI.gameObject.SetActive(true);
        m_timerUI.Reset_Timer();
    }

    public void Hide_Timer()
    {
        m_timerUI.gameObject.SetActive(false);
    }

    public void ShowTooltip(string name, string desc, Sprite sprite)
    {
        m_tootipUI.ShowToolTip(name, desc, sprite);
    }

    public void HideTooltip()
    {
        m_tootipUI.HideTooltip();
    }

    public void Use_Option(List<ResourceAmount> resource)
    {
        m_tootipUI.Use_Option(resource);
    }
}
