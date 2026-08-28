using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Commands;

public class UpgradeEffectRegistry
{
    private Dictionary<UpgradeEffectType, IUpgradeEffect> m_UpgradeEffects;

    public UpgradeEffectRegistry()
    {
        m_UpgradeEffects = new Dictionary<UpgradeEffectType, IUpgradeEffect>();

        m_UpgradeEffects[UpgradeEffectType.ExtractorAdjacency] = new ExtractorAdjEffect();
        m_UpgradeEffects[UpgradeEffectType.SmeltorAdjacency] = new SmeltorAdjEffect();
        m_UpgradeEffects[UpgradeEffectType.CrafterAdjacency] = new CrafterAdjEffect();
        m_UpgradeEffects[UpgradeEffectType.VentUpgrade] = new VentUpgradeEffect();
    }

    public void Apply(UpgradeEffectType effectType, Building building)
    {
        m_UpgradeEffects[effectType].Apply(building);
    }
}
