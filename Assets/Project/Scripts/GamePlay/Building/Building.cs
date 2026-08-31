using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public abstract class Building : MonoBehaviour
{
    [Header("Common Data")]
    public BuildingData m_data;
    public Vector3Int m_vecOriginCellPos;
    public string m_buildingName;

    protected float m_fCurrentHP;
    [SerializeField]protected float m_fMaxHP = 100f;

    [Header("Play Data")]
    [SerializeField] protected Vector3Int[] m_nearTiles = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };
    public List<Vector3Int> m_nearCellList;

    public virtual void Init(BuildingData data, Vector3Int vecCellPos, string Name)
    {
        m_data = data;
        m_vecOriginCellPos = vecCellPos;
        m_buildingName = Name;
        m_fCurrentHP = m_fMaxHP;

        m_nearCellList = new List<Vector3Int>();
        Set_NearCellPos();
    }

    //인접 타일 설정
    private void Set_NearCellPos()
    {
        foreach (Vector3Int NearDir in m_nearTiles)
        {
            m_nearCellList.Add(m_vecOriginCellPos + NearDir);
        }
    }

    //인접한 타일 가져오기
    public List<Vector3Int> Get_NearCellPos()
    {        
        return m_nearCellList;
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
