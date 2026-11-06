using UnityEngine;
using System.Collections.Generic;

public class UpgradeDatabase : Singleton<UpgradeDatabase>
{
    [SerializeField]
    List<UpgradeData> m_upgradeDatas = new List<UpgradeData>();
    Dictionary<int, UpgradeData> m_upgradeTables = new Dictionary<int, UpgradeData>();

    protected override void Awake()
    {
        base.Awake();
        Create_Upgradetable();       
    }

    public void Create_Upgradetable()
    {
        foreach(var data in m_upgradeDatas)
        {
            m_upgradeTables.Add(data.m_iUpgradeID, data);
        }
    }

    public UpgradeData Get_UpgradeData(int iID)
    {
        return m_upgradeTables[iID];
    }
}
