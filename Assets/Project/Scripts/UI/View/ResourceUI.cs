using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private TextMeshProUGUI m_TMPText;

    [Header("Setting")]
    [SerializeField] private ItemData m_ItemData;

    private void Awake()
    {
        m_TMPText = GetComponentInChildren<TextMeshProUGUI>();

        GetComponent<Image>().sprite = m_ItemData.m_iconSprite;      
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceChanged += Change_Amount;
        m_TMPText.text = ResourceManager.Instance.Get_ResourceAmount(m_ItemData).ToString();
    }

    public void Change_Amount(ItemData data, int iAmount)
    {
        if(data == m_ItemData)
        {
            m_TMPText.text = iAmount.ToString();
        }
    }

    private void OnDestroy()
    {
        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.OnResourceChanged -= Change_Amount;
        }        
    }
}
