using UnityEngine;
using System.Collections.Generic;

public class Smeltor : Producer
{
    [Header("Upgrade Data")]
    [SerializeField] private UpgradeData m_SmeltorAdj = null;
    public bool m_bIsUpgradeActive = false;

    public void Update()
    {
        if (m_bIsUpgradeActive)
            m_iCurrentProduceAmount = m_iBaseProduceAmount + 1;
        else
            m_iCurrentProduceAmount = m_iBaseProduceAmount;

        Update_Produce();
    }
    public override void OnInteract()
    {
        return;
    }

    public override void RecalculateBonus()
    {
        if (!m_SmeltorAdj.m_bActivate)
            return;

        int iCount = 0;
        m_bIsUpgradeActive = false;

        foreach (Vector3Int Near in m_ListNearCell)
        {
            PlacedBuilding building = GridDataManager.Instance.Get_PlacedBuilding(Near);

            if (building != null && building.m_data.m_fHeatPerSecond > 0f)
            {
                iCount++;

                if (iCount >= 2)
                {
                    m_bIsUpgradeActive = true;
                    return;
                }
            }
         
        }
    }
}
