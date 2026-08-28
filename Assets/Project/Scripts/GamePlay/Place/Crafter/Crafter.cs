using UnityEngine;

public class Crafter : Producer
{
    [Header("Upgrade Data")]
    [SerializeField] private UpgradeData m_CrafterAdj = null;
    public bool m_bIsUpgradeActive = false;

    public void Update()
    {
        
        m_iCurrentProduceAmount = m_iBaseProduceAmount + m_iBonusProduceAmount;

        Update_Produce();
    }
    public override void OnInteract()
    {
        return;
    }

    public override void RecalculateBonus()
    {
        foreach (UpgradeData upgrade in m_Data.m_upgradeList)
        {
            if (upgrade.m_bActivate)
                UpgradeManager.Instance.Upgrade_Apply(upgrade.Get_EffectType(), this);
        }
    }
}
