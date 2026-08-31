using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/Building Data/New BuildingData")]
public class BuildingData : ScriptableObject
{
    [Header("Base Info")]
    public int m_iBuildingID;
    public string m_buildingName;
    [TextArea]
    public string m_buildingDesc;
    public Sprite m_iconSprite;
    public GameObject m_goPrefab;

    [Header("Building Setting")]
    public int m_iHeight = 1;
    public int m_iWidth = 1;
    public List<ResourceAmount> m_totalCost;

    [Header("Penalty Setting")]
    public float m_fHeatPerSecond;
    public float m_fPollutionPerSecond;

    [Header("Upgrade List")]
    public List<UpgradeData> m_upgradeList;

    public virtual bool IsEnable_Spawn(Vector3Int vecOriginCellPos)
    { 
        for(int i =0; i<m_iWidth;  i++)
        {
            for(int j= 0; j<m_iHeight; j++)
            {
                Vector3Int vecCellPos = vecOriginCellPos + new Vector3Int(i, j, 0);

                if (GridDataManager.Instance.IsOccupied(vecCellPos))
                    return false;

                if (vecCellPos == Player.m_vecPlayerCellPos)
                    return false;
            }
        }       
    
        return true;
    }

    public virtual void Spawn_Instance(Vector3 vecWolrdPos, Vector3Int vecCellPos, RecipeData recipe)
    {
        GameObject goInstance = Instantiate(m_goPrefab, vecWolrdPos, Quaternion.identity);

        Building building = SetUp_Building(goInstance, vecCellPos, recipe);

        GridDataManager.Instance.Add_Object(vecCellPos, this, building);

        BillbordSprite billboard = goInstance.AddComponent<BillbordSprite>();

        SoundManager.Instance.PlaySFX(SoundManager.Instance.m_clickSound);

        if (billboard != null)
            billboard.Set_Billboard();
    }

    public virtual Building SetUp_Building(GameObject goInstance, Vector3Int vecCellPos, RecipeData recipe)
    {
        Building building = goInstance.GetComponent<Building>();

        building.Init(this, vecCellPos, m_buildingName);

        return building;
    }
}
