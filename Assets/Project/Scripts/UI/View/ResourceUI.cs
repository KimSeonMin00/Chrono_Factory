using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private TextMeshProUGUI m_tmpText;

    [Header("Setting")]
    [SerializeField] private ItemData m_itemData;

    private void Awake()
    {
        m_tmpText = GetComponentInChildren<TextMeshProUGUI>();

        GetComponent<Image>().sprite = m_itemData.m_iconSprite;      
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceChanged += Change_Amount;
        m_tmpText.text = ResourceManager.Instance.Get_ResourceAmount(m_itemData).ToString();
    }

    public void Change_Amount(ItemData data, int iAmount)
    {
        if(data == m_itemData)
        {
            m_tmpText.text = iAmount.ToString();
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
