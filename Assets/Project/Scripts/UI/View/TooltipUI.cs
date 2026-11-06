using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;
using System.Collections.Generic;

//건물, 업그레이드 정보 툴팁
public class TooltipUI : MonoBehaviour
{
    [SerializeField] private Image m_Sprite;
    [SerializeField] private TextMeshProUGUI m_NameText;
    [SerializeField] private TextMeshProUGUI m_DescText;

    [SerializeField] private List<CostUI> m_CostOption;//건물 정보를 볼때만 활성화

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

        Off_Option();
    }

    public void HideTooltip()
    {
        this.gameObject.SetActive(false);
    }

    public void Use_Option(List<ResourceAmount> resources)
    {
        Off_Option();

        for(int i =0; i < resources.Count; i++)
        {
            m_CostOption[i].Set_Data(resources[i]);
            m_CostOption[i].gameObject.SetActive(true);
        }
    }

    public void Off_Option()
    {
        foreach (var cost in m_CostOption)
            cost.gameObject.SetActive(false);
    }
}
