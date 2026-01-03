using UnityEngine;
using System.Collections.Generic;

public class GridDataManager : Singleton<GridDataManager>
{
    private Dictionary<Vector3Int, PlacedBuilding> m_placedObjects = new Dictionary<Vector3Int, PlacedBuilding>();

    public bool IsOccupied(Vector3Int vecCellPos)
    {
        return m_placedObjects.ContainsKey(vecCellPos);
    }

    public void Add_Object(Vector3Int vecCellPos, BuildingData data, Building PlacedBuilding)
    {
        if (IsOccupied(vecCellPos) == false)
        {
            m_placedObjects.Add(vecCellPos, new PlacedBuilding(data, PlacedBuilding));
        }
    }

    public PlacedBuilding Get_PlacedBuilding(Vector3Int vecCellPos)
    {
        m_placedObjects.TryGetValue(vecCellPos, out var building);
        return building;
    }

    public void Remove_Object(Vector3Int vecCellPos)
    {
        if (IsOccupied(vecCellPos) == true)
        {
            if (m_placedObjects.TryGetValue(vecCellPos, out var building))
            {
                building.m_placedBuilding.OnDestroyed();
                m_placedObjects.Remove(vecCellPos);
            }
        }
    }
}
