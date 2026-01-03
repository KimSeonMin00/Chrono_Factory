using UnityEngine;

[System.Serializable]
public class PlacedBuilding
{
    public BuildingData m_data;
    public Building m_placedBuilding;

    public PlacedBuilding(BuildingData data, Building placedBuilding)
    {
        this.m_data = data;
        this.m_placedBuilding = placedBuilding;
    }
}
