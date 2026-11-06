using UnityEngine;

[CreateAssetMenu(fileName = "ExtractorData", menuName = "Scriptable Objects/Building Data/New ExtractorData")]
public class ExtractorData : BuildingData
{

    public override bool IsEnable_Spawn(Vector3Int vecCellPos)
    {
        if (!base.IsEnable_Spawn(vecCellPos))
            return false;

        if (GridDataManager.Instance.Get_ResourceOnTile(vecCellPos) != null)
        {
            return true;
        }
        else
            return false;
    }

    public override Building SetUp_Building(GameObject goInstance, Vector3Int vecCellPos, RecipeData recipe)
    {
        Extractor building = goInstance.GetComponent<Extractor>();

        building.Init(this, vecCellPos, m_buildingName);

        building.SetUp_Resource(GridDataManager.Instance.Get_ResourceOnTile(vecCellPos));

        return building;
    }
}
