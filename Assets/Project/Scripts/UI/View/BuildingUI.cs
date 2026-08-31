using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private Image m_buildingImage;
    [Header("Data")]
    [SerializeField] private BuildingData m_buildingData;
    [SerializeField] private List<RecipeData> m_recipeDataList;

    [Header("List")]
    [SerializeField] private RecipeUIList m_recipeUIList;
    

    private void Awake()
    {
        if (m_buildingData != null)
        {
            m_buildingImage = GetComponentsInChildren<Image>()[1];
            m_buildingImage.sprite = m_buildingData.m_iconSprite;
        }

        if (m_recipeDataList.Count != 0)
            Set_Recipe();
    }

    public void Set_Recipe()
    {
        m_recipeUIList.Set_Recipe(m_buildingData, m_recipeDataList);
    }
    public void Set_Building()
    {
        PlacementInfo placeInfo = new PlacementInfo();
        placeInfo.m_buildingData = m_buildingData;
        placeInfo.m_recipeData = null;
        PlacementController.Instance.Set_BuildingData(placeInfo);
    }

    public void On_Clicked()
    {
        if (m_recipeDataList.Count == 0)
            Set_Building();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ShowTooltip(m_buildingData.m_buildingName, m_buildingData.m_buildingDesc, m_buildingData.m_iconSprite);
        UIManager.Instance.Use_Option(m_buildingData.m_totalCost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideTooltip();
    }
}
