using UnityEngine;

public class CrafterAdjEffect : IUpgradeEffect
{
    public void Apply(Building building)
    {
        Crafter crafter = building as Crafter;

        if (crafter == null)
            return;

        bool m_bIsNearIron = false;
        bool m_bIsNearCrystal = false;

        crafter.m_iBonusProduceAmount = 0;

        foreach (Vector3Int cell in crafter.Get_NearCellPos())
        {
            PlacedBuilding placed =
                GridDataManager.Instance.Get_PlacedBuilding(cell);

            if(placed != null &&placed.m_building is Producer)
            {
                Producer producer = placed.m_building as Producer;

                switch (producer.Get_Recipe().m_outputResources.m_itemData.m_itemName)
                {
                    case "IronIngot":
                        m_bIsNearIron = true;
                        break;

                    case "Crystal":
                        m_bIsNearCrystal = true;
                        break;

                    default:
                        break;
                }

                if (m_bIsNearCrystal && m_bIsNearIron)
                    crafter.m_iBonusProduceAmount = 2;
            }


        }
    }
}
