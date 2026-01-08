using UnityEngine;

public class Extractor : Building
{
    [SerializeField]private ResourceType m_Resourcetype = ResourceType.None;

    public float m_fProduceCooldown = 1f;
    public float m_fTime = 0f;

    public int m_iCount = 0;
    private void Update()
    {
        m_fTime += Time.deltaTime;
        if(m_fTime >= m_fProduceCooldown)
        {
            ResourceManager.Instance.Add_Resource(m_Resourcetype, 1);
            m_fTime = 0;
        }
    }
    public override void OnInteract()
    {
        return;
    }

    public void SetUp_Resource(ResourceType type)
    {
        m_Resourcetype = type;
    }

    public override void RecalculateBonus()
    {
        int iCount = 0;

        foreach(Vector3Int Near in m_NearTile)
        {
            PlacedBuilding building = GridDataManager.Instance.Get_PlacedBuilding(m_vecCellPos + Near);

            if (building != null && building.m_placedBuilding is Extractor)
                iCount++;
        }

        m_iCount = iCount;
    }
}
