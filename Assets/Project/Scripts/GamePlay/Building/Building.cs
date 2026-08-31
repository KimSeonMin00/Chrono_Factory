using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public abstract class Building : MonoBehaviour
{
    [Header("Common Data")]
    public BuildingData m_Data;
    public Vector3Int m_vecOriginCellPos;
    public string m_BuildingName;

    protected float m_fCurrentHP;
    [SerializeField]protected float m_fMaxHP = 100f;

    [Header("Play Data")]
    [SerializeField] protected Vector3Int[] m_NearTile = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };
    public List<Vector3Int> m_ListNearCell;

    public virtual void Init(BuildingData data, Vector3Int vecCellPos, string Name)
    {
        m_Data = data;
        m_vecOriginCellPos = vecCellPos;
        m_BuildingName = Name;
        m_fCurrentHP = m_fMaxHP;

        m_ListNearCell = new List<Vector3Int>();
        Set_NearCellPos();
    }

    private void Set_NearCellPos()
    {
        foreach (Vector3Int NearDir in m_NearTile)
        {
            m_ListNearCell.Add(m_vecOriginCellPos + NearDir);
        }
    }
    public List<Vector3Int> Get_NearCellPos()
    {        
        return m_ListNearCell;
    }

    public abstract void RecalculateBonus();

    public virtual void OnNearbyProduction(Building producer)
    {
    }
    public abstract void OnInteract();

    public virtual void OnDestroyed()
    {
        Destroy(gameObject);
        StopAllCoroutines();
    }   
}
