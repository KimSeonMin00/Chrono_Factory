using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Commands;

public class UpgradeEffectRegistry
{
    private Dictionary<UpgradeEffectType, IUpgradeEffect> m_UpgradeEffects;

    public UpgradeEffectRegistry()//생성시 Type에 대응하는 effect 객체 생성
    {
        m_UpgradeEffects = new Dictionary<UpgradeEffectType, IUpgradeEffect>();

        m_UpgradeEffects[UpgradeEffectType.ExtractorAdjacency] = new ExtractorAdjEffect();
        m_UpgradeEffects[UpgradeEffectType.SmeltorAdjacency] = new SmeltorAdjEffect();
        m_UpgradeEffects[UpgradeEffectType.CrafterAdjacency] = new CrafterAdjEffect();
        m_UpgradeEffects[UpgradeEffectType.VentUpgrade] = new VentUpgradeEffect();
        m_UpgradeEffects[UpgradeEffectType.CoolerAdjacency] = new CoolerAdjEffect();
    }

    public void Apply(UpgradeEffectType effectType, Building building)
    {
        m_UpgradeEffects[effectType].Apply(building);//실제 Effect 적용
    }
}
