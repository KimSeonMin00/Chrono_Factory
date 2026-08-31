using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private Image m_sprite;

    public void Set_Data(ResourceAmount resource)
    {
        m_sprite.sprite = resource.m_itemData.m_iconSprite;
        m_text.text = resource.m_iAmount.ToString();    
    }

}
