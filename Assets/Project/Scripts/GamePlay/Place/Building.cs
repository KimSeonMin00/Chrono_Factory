using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [Header("Common Data")]
    public BuildingData m_Data;
    public Vector3Int m_vecCellPos;
    public string m_BuildingName;

    protected float m_fCurrentHP;
    [SerializeField]protected float m_fMaxHP = 100f;

    public virtual void Init(BuildingData data, Vector3Int vecCellPos, string Name)
    {
        m_Data = data;
        m_vecCellPos = vecCellPos;
        m_BuildingName = Name;
        m_fCurrentHP = m_fMaxHP;
    }

    public abstract void OnInteract();

    public virtual void OnDestroyed()
    {
        Destroy(gameObject);
    }
}
