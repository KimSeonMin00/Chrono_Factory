using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private Image m_BuildingImage;
    [Header("Data")]
    [SerializeField] private BuildingData m_buildingData;
    [SerializeField] private List<RecipeData> m_RecipeList;

    [Header("List")]
    [SerializeField] private RecipeUIList m_RecipeUIList;
    

    private void Awake()
    {
        if (m_buildingData != null)
        {
            m_BuildingImage = GetComponentsInChildren<Image>()[1];
            m_BuildingImage.sprite = m_buildingData.m_IconSprite;
        }

        if (m_RecipeList.Count != 0)
            Set_Recipe();
    }

    public void Set_Recipe()
    {
        m_RecipeUIList.Set_Recipe(m_buildingData, m_RecipeList);
    }
    public void Set_Building()
    {
        PlacementInfo placeInfo = new PlacementInfo();
        placeInfo.m_BuildingData = m_buildingData;
        placeInfo.m_RecipeData = null;
        PlacementController.Instance.Set_BuildingData(placeInfo);
    }

    public void On_Clicked()
    {
        if (m_RecipeList.Count == 0)
            Set_Building();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ShowTooltip(m_buildingData.m_BuildingName, m_buildingData.m_BuildingDesc, m_buildingData.m_IconSprite);
        UIManager.Instance.Use_Option(m_buildingData.m_Cost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideTooltip();
    }
}
