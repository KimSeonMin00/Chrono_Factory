using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    [Header("Referencces")]
    [SerializeField] private Image m_BuildingImage;
    [Header("Data")]
    [SerializeField] private BuildingData m_buildingData;
    [SerializeField] private RecipeData m_RecipeData;

    private void Awake()
    {
        if (m_buildingData != null)
        {
            m_BuildingImage = GetComponentsInChildren<Image>()[1];
            m_BuildingImage.sprite = m_buildingData.m_goPrefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void Set_Building()
    {
        PlacementInfo placeInfo = new PlacementInfo();
        placeInfo.m_BuildingData = m_buildingData;
        placeInfo.m_RecipeData = m_RecipeData;
        PlacementController.Instance.Set_BuildingData(placeInfo);
    }
}
