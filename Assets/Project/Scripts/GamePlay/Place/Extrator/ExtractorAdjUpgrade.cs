using UnityEngine;
public class ExtractorAdjEffect : IUpgradeEffect
{
    public void Apply(Building building)
    {
        Extractor extractor = building as Extractor;

        if (extractor == null)
            return;

        int count = 0;

        foreach (Vector3Int cell in extractor.Get_NearCellPos())
        {
            PlacedBuilding placed =
                GridDataManager.Instance.Get_PlacedBuilding(cell);

            if (placed != null &&
                placed.m_building is Extractor)
            {
                count++;
            }
        }

        extractor.Set_Bonus(count);
    }
}