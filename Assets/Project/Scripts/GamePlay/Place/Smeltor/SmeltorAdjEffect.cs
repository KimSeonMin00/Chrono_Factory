using UnityEngine;

public class SmeltorAdjEffect : IUpgradeEffect
{
    public void Apply(Building building)
    {
        Smeltor smeltor = building as Smeltor;

        if (smeltor == null)
            return;

        int count = 0;

        smeltor.m_iBonusProduceAmount = 0;

        foreach (Vector3Int cell in smeltor.Get_NearCellPos())
        {
            PlacedBuilding placed =
                GridDataManager.Instance.Get_PlacedBuilding(cell);

            if (placed != null &&
                placed.m_data.m_fHeatPerSecond > 0f)
            {
                count++;
            }

            if(count >= 2)
            {
                smeltor.m_iBonusProduceAmount = 1;
                break;
            }
        }
    }
}
