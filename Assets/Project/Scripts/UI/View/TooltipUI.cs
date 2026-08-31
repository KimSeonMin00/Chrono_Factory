using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;
using System.Collections.Generic;

public class TooltipUI : MonoBehaviour
{
    [SerializeField] private Image m_sprite;
    [SerializeField] private TextMeshProUGUI m_tmpNameText;
    [SerializeField] private TextMeshProUGUI m_tmpDescText;

    [SerializeField] private List<CostUI> m_costOption;

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

        m_tmpNameText.text = name;
        m_tmpDescText.text = desc;
        m_sprite.sprite = sprite;

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
            m_costOption[i].Set_Data(resources[i]);
            m_costOption[i].gameObject.SetActive(true);
        }
    }

    public void Off_Option()
    {
        foreach (var cost in m_costOption)
            cost.gameObject.SetActive(false);
    }
}
