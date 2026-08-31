using UnityEngine;

[System.Serializable]

//건물 데이터와 실제 building오브젝트를 따로 관리하기 위해 만든 중간 클래스
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
