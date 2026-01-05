using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private TextMeshProUGUI m_TMPText;

    [Header("Setting")]
    [SerializeField] private ResourceType m_resourceType;

    private void Awake()
    {
        m_TMPText = GetComponentInChildren<TextMeshProUGUI>();

        ResourceManager.Instance.OnResourceChanged += Change_Amount;
    }

    public void Change_Amount(ResourceType type, int iAmount)
    {
        if(type == m_resourceType)
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
