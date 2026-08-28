using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : Singleton<ItemDatabase>
{
    [SerializeField]
    List<ItemData> m_itemDatas = new List<ItemData>();

    Dictionary<int, ItemData> m_itemtables = new Dictionary<int, ItemData>();

    protected override void Awake()
    {
        base.Awake();
        Create_Itemtable();
    }

    public void Create_Itemtable()
    {
        foreach (var data in m_itemDatas)
        {
            m_itemtables.Add(data.m_iItemID, data);
        }
    }

    public ItemData Get_ItemData(int iItemID)
    {
        return m_itemtables[iItemID];  
    }
}
