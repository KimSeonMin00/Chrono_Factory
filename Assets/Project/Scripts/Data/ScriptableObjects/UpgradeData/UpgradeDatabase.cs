using UnityEngine;
using System.Collections.Generic;

public class UpgradeDatabase : Singleton<UpgradeDatabase>
{
    [SerializeField]
    List<UpgradeData> m_UpgradeDatas = new List<UpgradeData>();
    Dictionary<int, UpgradeData> m_Upgradetables = new Dictionary<int, UpgradeData>();

    protected override void Awake()
    {
        base.Awake();
        
    }

    public void Create_Upgradetable()
    {
        foreach(var data in m_UpgradeDatas)
        {
            m_Upgradetables.Add(data.m_iUpgradeID, data);
        }
    }
}
