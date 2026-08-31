using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Commands;

public class UpgradeEffectRegistry
{
    private Dictionary<UpgradeEffectType, IUpgradeEffect> m_upgradeEffects;

    public UpgradeEffectRegistry()//생성시 Type에 대응하는 effect 객체 생성
    {
        m_upgradeEffects = new Dictionary<UpgradeEffectType, IUpgradeEffect>();

        m_upgradeEffects[UpgradeEffectType.ExtractorAdjacency] = new ExtractorAdjEffect();
        m_upgradeEffects[UpgradeEffectType.SmeltorAdjacency] = new SmeltorAdjEffect();
        m_upgradeEffects[UpgradeEffectType.CrafterAdjacency] = new CrafterAdjEffect();
        m_upgradeEffects[UpgradeEffectType.VentUpgrade] = new VentUpgradeEffect();
        m_upgradeEffects[UpgradeEffectType.CoolerAdjacency] = new CoolerAdjEffect();
    }

    public void Apply(UpgradeEffectType effectType, Building building)
    {
        m_upgradeEffects[effectType].Apply(building);//실제 Effect 적용
    }
}
