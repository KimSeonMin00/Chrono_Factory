using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image m_OutputImage;

    [SerializeField]private PlacementInfo m_Placement;

    private void Awake()
    {
        m_Placement = new PlacementInfo();
    }
    public void Set_InfoToController()
    {
        PlacementController.Instance.Set_BuildingData(m_Placement);
    }

    public void Set_Info(BuildingData building, RecipeData recipe)
    {
        m_Placement.m_BuildingData = building;
        m_Placement.m_RecipeData = recipe;

        m_OutputImage = GetComponentsInChildren<Image>()[1];
        m_OutputImage.sprite = recipe.m_OutputSprite;
    }

}