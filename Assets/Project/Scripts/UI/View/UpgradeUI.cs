using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image m_upgradeImage;
    [SerializeField] private TextMeshProUGUI m_tmpCost;
    public UpgradeData m_UpgradeData;

    private void Awake()
    {
        if (m_UpgradeData != null)
        {
            m_upgradeImage.sprite = m_UpgradeData.m_iconSprite;
            m_tmpCost.text = m_UpgradeData.Get_Cost().ToString();

            if (m_UpgradeData.m_bActivate)
            {
                m_tmpCost.text = "OK!";
                m_tmpCost.color = Color.green;
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ShowTooltip(m_UpgradeData.name, m_UpgradeData.m_upgradeDesc, m_UpgradeData.m_iconSprite);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideTooltip();
    }
    public void OnClick_Button()
    {
        if (!UpgradeManager.Instance.Try_Upgrade(m_UpgradeData))
            return;

        SoundManager.Instance.PlaySFX(SoundManager.Instance.m_upgradeSound);
        if (m_UpgradeData.m_bActivate)
        {
            m_tmpCost.text = "OK!";
            m_tmpCost.color = Color.green;
        }
    }
}
