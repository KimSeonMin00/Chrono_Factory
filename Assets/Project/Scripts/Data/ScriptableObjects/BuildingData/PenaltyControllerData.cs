using UnityEngine;

[CreateAssetMenu(fileName = "PenaltyControllerData", menuName = "Scriptable Objects/Building Data/New PenaltyControllerData")]
public class PenaltyControllerData : BuildingData
{
    [Header("PenaltyController Setting")]
    public float m_fConsumeHeat;
    public float m_fConsumePollution;

    public override bool IsEnable_Spawn(Vector3Int vecCellPos)
    {
        if (!base.IsEnable_Spawn(vecCellPos))
            return false;

        return true;
    }

    public override Building SetUp_Building(GameObject goInstance, Vector3Int vecCellPos, RecipeData recipe)
    {
        PenaltyController penaltyController = goInstance.GetComponent<PenaltyController>(); 

        penaltyController.Init(this, vecCellPos, m_buildingName);

        penaltyController.Setup(m_fConsumeHeat, m_fConsumePollution);

        return penaltyController;
    }
}
