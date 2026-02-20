using System.Collections.Generic;
using UnityEngine;

public class Cooler : PenaltyController
{
    public int m_iProduceCount = 0;
    public List<Producer> m_NearProducers;

    [Header("Upgrade Data")]
    [SerializeField] private UpgradeData m_CoolerAdj = null;
    public bool m_bIsUpgradeActive = false;

    public override void Init(BuildingData data, Vector3Int vecCellPos, string Name)
    {
        base.Init(data, vecCellPos, Name);

        m_NearProducers = new List<Producer>();
    }

    public override void OnInteract()
    {
        return;
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();

        foreach (var producer in m_NearProducers)
            producer.OnProduced -= Produce_Detect;

        m_NearProducers.Clear();
    }

    public override void RecalculateBonus()
    {
        if (!m_CoolerAdj.m_bActivate)
            return;

        foreach (var producer in m_NearProducers)
            producer.OnProduced -= Produce_Detect;

        m_NearProducers.Clear();

        foreach (Vector3Int Near in m_ListNearCell)
        {
            PlacedBuilding building = GridDataManager.Instance.Get_PlacedBuilding(Near);

            if (building != null && building.m_placedBuilding is Producer)
            {
                Producer producer = building.m_placedBuilding as Producer;

                m_NearProducers.Add(producer);
                producer.OnProduced += Produce_Detect;
            }

        }
    }

    public void Produce_Detect()
    {
        m_iProduceCount++;

        if(m_iProduceCount >= 10)
        {
            Debug.Log("Buff");
            m_iProduceCount = 0;
        }
    }
}
