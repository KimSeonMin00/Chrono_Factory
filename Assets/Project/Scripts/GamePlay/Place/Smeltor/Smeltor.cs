using Mono.Cecil;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Smeltor : Producer
{
    [Header("Upgrade Data")]
    [SerializeField] private UpgradeData m_ExtratorAdj = null;
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
        int iCount = 0;
        m_bIsUpgradeActive = false;

        foreach (Vector3Int Near in m_NearTile)
        {
            PlacedBuilding building = GridDataManager.Instance.Get_PlacedBuilding(m_vecCellPos + Near);

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
