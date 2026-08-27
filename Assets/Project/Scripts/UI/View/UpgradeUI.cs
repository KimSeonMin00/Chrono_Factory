using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image m_UpgradeImage;
    [SerializeField] private TextMeshProUGUI m_Cost;
    public UpgradeData m_UpgradeData;

    private void Awake()
    {
        if (m_UpgradeData != null)
        {
            m_UpgradeImage.sprite = m_UpgradeData.m_IconSprite;
            m_Cost.text = m_UpgradeData.Get_Cost().ToString();

            if (m_UpgradeData.m_bActivate)
            {
                m_Cost.text = "OK!";
                m_Cost.color = Color.green;
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ShowTooltip(m_UpgradeData.name, m_UpgradeData.m_UpgradeDesc, m_UpgradeData.m_IconSprite);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideTooltip();
    }
    public void OnClick_Button()
    {
        if (!UpgradeManager.Instance.Try_Upgrade(m_UpgradeData))
            return;

        SoundManager.Instance.PlaySFX(SoundManager.Instance.m_UpgradeSound);
        if (m_UpgradeData.m_bActivate)
        {
            m_Cost.text = "OK!";
            m_Cost.color = Color.green;
        }
    }
}
