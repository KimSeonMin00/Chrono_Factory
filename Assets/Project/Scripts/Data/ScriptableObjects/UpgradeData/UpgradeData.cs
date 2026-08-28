using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]

public class UpgradeData : ScriptableObject
{
    [SerializeField] private string m_UpgradeName;
    [TextArea]
    public string m_UpgradeDesc;
    public Sprite m_IconSprite;
    [SerializeField] private bool bActivate = false;
    public bool m_bActivate => bActivate;

    [SerializeField] private int iLevel = 1;

    [SerializeField] private int iCost;

    [SerializeField] private UpgradeEffectType m_upgradeEffectType;
    public int m_iLevel => iLevel;

    public void Upgrade_Level()
    {
        if (!bActivate)
        {
            bActivate = true;
        }

        else
           iLevel++;
    }

    public int Get_Cost()
    {
        return iCost;
    }

    public void Reset_Level()
    {
        bActivate = false;
        iLevel = 1;
    }

    public UpgradeEffectType Get_EffectType()
    {
        return m_upgradeEffectType;
    }
}


public enum UpgradeEffectType
{
    ExtractorAdjacency,
    SmeltorAdjacency,
    CrafterAdjacency,
    VentUpgrade,
}

