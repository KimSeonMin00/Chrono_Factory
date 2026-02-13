using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] StorageUI m_storageUI;
    [SerializeField] TooltipUI m_tootipUI;
    [SerializeField] TimerUI m_timerUI;

    public void OpenUI()
    {
        m_storageUI.gameObject.SetActive(true);
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
}
