using UnityEngine;

public class Extractor : Building
{
    [SerializeField] private ItemData m_itemdata = null;

    [Header("Upgrade Data")]
    [SerializeField] private UpgradeData m_ExtratorAdj = null;

    [Header("Building Setting")]
    public float m_fProduceCooldown = 1f;
    public float m_fTime = 0f;

    public int m_iCount = 0;
    void Update()
    {
        m_fTime += Time.deltaTime;

        ResourceManager.Instance.Add_Heat(m_Data.m_fHeatPerSecond * Time.deltaTime);
        ResourceManager.Instance.Add_Pollution(m_Data.m_fPollutionPerSecond * Time.deltaTime);

        if (m_fTime >= m_fProduceCooldown)
        {
            ResourceManager.Instance.Add_Resource(m_itemdata, 1);
            if(m_ExtratorAdj.m_bActivate)
                ResourceManager.Instance.Add_Resource(m_itemdata, m_iCount);

            ResourceManager.Instance.Produce_Effect(m_itemdata, transform.position);

            m_fTime = 0;
        }
    }
    public override void OnInteract()
    {
        return;
    }

    public void SetUp_Resource(ItemData data)
    {
        m_itemdata = data;
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
