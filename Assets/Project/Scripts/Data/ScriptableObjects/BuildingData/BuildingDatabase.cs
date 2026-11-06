using UnityEngine;
using System.Collections.Generic;

//building data를 id로 관리, Save/Load에 사용(현재 사용 X)
public class BuildingDatabase : Singleton<BuildingDatabase>
{
    [SerializeField]
    List<BuildingData> m_buildingDatas = new List<BuildingData>();

    Dictionary<int, BuildingData> m_buildingtables = new Dictionary<int, BuildingData>();

    protected override void Awake()
    {
        base.Awake();
        Create_Buildingtable();
    }

    public void Create_Buildingtable()
    {
        foreach (var data in m_buildingDatas)
        {
            m_buildingtables.Add(data.m_iBuildingID, data);
        }
    }
}
