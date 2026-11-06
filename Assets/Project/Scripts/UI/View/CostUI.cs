using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Image m_sprite;

    public void Set_Data(ResourceAmount resource)
    {
        m_sprite.sprite = resource.m_item.m_iconSprite;
        m_Text.text = resource.m_iAmount.ToString();    
    }

}
