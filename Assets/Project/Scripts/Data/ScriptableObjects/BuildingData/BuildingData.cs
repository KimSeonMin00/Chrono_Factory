using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/Building Data/New BuildingData")]
public class BuildingData : ScriptableObject
{
    [Header("Base Info")]
    public string m_BuildingName;
    [TextArea]
    public string m_BuildingDesc;
    public Sprite m_IconSprite;
    public GameObject m_goPrefab;

    [Header("Building Setting")]
    public int m_iHeight = 1;
    public int m_iWidth = 1;
    public List<ResourceAmount> m_Cost;

    [Header("Penalty Setting")]
    public float m_fHeatPerSecond;
    public float m_fPollutionPerSecond;

    public virtual bool IsEnable_Spawn(Vector3Int vecCellPos)
    {
        if (GridDataManager.Instance.IsOccupied(vecCellPos))
            return false;

        if (vecCellPos == Player.m_vecPlayerCellPos)
            return false;

        return true;
    }

    public virtual void Spawn_Instance(Vector3 vecWolrdPos, Vector3Int vecCellPos, RecipeData recipe)
    {
        GameObject goInstance = Instantiate(m_goPrefab, vecWolrdPos, Quaternion.identity);

        Building building = SetUp_Building(goInstance, vecCellPos, recipe);

        GridDataManager.Instance.Add_Object(vecCellPos, this, building);

        BillbordSprite billboard = goInstance.AddComponent<BillbordSprite>();

        if (billboard != null)
            billboard.Set_Billboard();
    }

    public virtual Building SetUp_Building(GameObject goInstance, Vector3Int vecCellPos, RecipeData recipe)
    {
        Building building = goInstance.GetComponent<Building>();

        building.Init(this, vecCellPos, m_BuildingName);

        return building;
    }
}
