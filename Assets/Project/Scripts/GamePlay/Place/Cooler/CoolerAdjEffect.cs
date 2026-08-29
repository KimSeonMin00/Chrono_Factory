using UnityEngine;

public class CoolerAdjEffect : IUpgradeEffect
{
    public void Apply(Building building)
    {
        Cooler cooler = building as Cooler;

        if (cooler != null)        
            return;
        
        foreach(Vector3Int cell in cooler.Get_NearCellPos())
        {
            PlacedBuilding placed =
                GridDataManager.Instance.Get_PlacedBuilding(cell);

            if (placed != null &&
                placed.m_building is Producer)
            {
                Producer producer= placed.m_building as Producer;

                producer.Haste();
            }
        }
    }
}
