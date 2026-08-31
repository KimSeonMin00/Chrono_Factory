using System.Collections.Generic;
using UnityEngine;

public class Cooler : PenaltyController
{
    public int m_iProduceCount = 0;

    [Header("Upgrade Data")]
    [SerializeField] private UpgradeData m_CoolerAdj = null;
    public bool m_bIsUpgradeActive = false;

    public override void Init(BuildingData data, Vector3Int vecCellPos, string Name)
    {
        base.Init(data, vecCellPos, Name);

    }

    public override void OnInteract()
    {
        return;
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
    }

    public override void RecalculateBonus()
    {
        
    }

    public override void OnNearbyProduction(Building producer)
    {

        if (!m_CoolerAdj.m_bActivate)
            return;

        m_iProduceCount++;

        if (m_iProduceCount >= 50)
        {
            foreach (UpgradeData upgrade in m_data.m_upgradeList)
            {
                if (upgrade.m_bActivate)
                {
                    UpgradeManager.Instance.Upgrade_Apply(upgrade.Get_EffectType(), this);
                }
            }

            m_iProduceCount = 0;
        }
    }
}
