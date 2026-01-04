using UnityEngine;

[CreateAssetMenu(fileName = "ExtractorData", menuName = "Scriptable Objects/Building Data/New ExtractorData")]
public class ExtractorData : BuildingData
{

    public override bool IsEnable_Spawn(Vector3Int vecCellPos)
    {
        base.IsEnable_Spawn(vecCellPos);

        if (GridDataManager.Instance.Get_ResourceOnTile(vecCellPos) != ResourceType.None)
        {
            return true;
        }
        else
            return false;
    }

    public override Building SetUp_Building(GameObject goInstance, Vector3Int vecCellPos)
    {
        Extractor building = goInstance.GetComponent<Extractor>();

        building.Init(this, vecCellPos, m_BuildingName);

        building.SetUp_Resource(GridDataManager.Instance.Get_ResourceOnTile(vecCellPos));

        return building;
    }
}
