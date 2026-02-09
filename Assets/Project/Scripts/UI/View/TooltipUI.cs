using TMPro;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    [SerializeField] private Image m_Sprite;
    [SerializeField] private TextMeshProUGUI m_NameText;
    [SerializeField] private TextMeshProUGUI m_DescText;

    private RectTransform m_rectTrasform;

    void Awake()
    {
        m_rectTrasform = GetComponent<RectTransform>();
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            m_rectTrasform.position = InputManager.Instance.m_MousePos;
        }       
    }

    public void ShowToolTip(string name, string desc, Sprite sprite)
    {
        this.gameObject.SetActive(true);

        m_NameText.text = name;
        m_DescText.text = desc;
        m_Sprite.sprite = sprite;
    }

    public void HideTooltip()
    {
        this.gameObject.SetActive(false);
    }
}
