using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/Building Data/New BuildingData")]
public class BuildingData : ScriptableObject
{
    public string m_BuildingName;
    public GameObject m_goPrefab;

    public virtual bool IsEnable_Spawn(Vector3Int vecCellPos)
    {
        if (GridDataManager.Instance.IsOccupied(vecCellPos))
            return false;

        return true;
    }

    public virtual void Spawn_Instance(Vector3 vecWolrdPos, Vector3Int vecCellPos)
    {
        GameObject goInstance = Instantiate(m_goPrefab, vecWolrdPos, Quaternion.identity);

        Building building = SetUp_Building(goInstance, vecCellPos);

        GridDataManager.Instance.Add_Object(vecCellPos, this, building);

        BillbordSprite billboard = goInstance.AddComponent<BillbordSprite>();

        if (billboard != null)
            billboard.Set_Billboard();
    }

    public virtual Building SetUp_Building(GameObject goInstance, Vector3Int vecCellPos)
    {
        Building building = goInstance.GetComponent<Building>();

        building.Init(this, vecCellPos, m_BuildingName);

        return building;
    }
}
