using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image m_outputImage;

    [SerializeField]private PlacementInfo m_placementInfo;

    private void Awake()
    {
        m_placementInfo = new PlacementInfo();
    }
    public void Set_InfoToController()
    {
        PlacementController.Instance.Set_BuildingData(m_placementInfo);
    }

    public void Set_Info(BuildingData building, RecipeData recipe)
    {
        m_placementInfo.m_buildingData = building;
        m_placementInfo.m_recipeData = recipe;

        m_outputImage = GetComponentsInChildren<Image>()[1];
        m_outputImage.sprite = recipe.m_outputSprite;
    }

}