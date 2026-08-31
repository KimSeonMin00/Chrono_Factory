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
            /*
             * 업그레이드 효과 적용시 모든 data를 순회하며 활성화시 Apply하는 방식
             * 현재 하나의 건물에 다른 트리거를 가진 업그레이드가 여러개 있는 경우가 대비되어 있지 않음
             * 추후 적용방식을 수정할 가능성 있음
             */
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
