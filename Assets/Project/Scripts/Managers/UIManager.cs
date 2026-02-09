using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] StorageUI m_storageUI;
    [SerializeField] TooltipUI m_tootipUI;

    public void OpenUI()
    {
        m_storageUI.gameObject.SetActive(true);
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
