using UnityEngine;

[System.Serializable]
public class PlacedBuilding
{
    public BuildingData m_data;
    public Building m_building;

    public PlacedBuilding(BuildingData data, Building building)
    {
        this.m_data = data;
        this.m_building = building;
    }
}
