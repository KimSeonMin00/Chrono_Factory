using UnityEngine;

public class Crafter : Producer
{
    [Header("Upgrade Data")]
    [SerializeField] private UpgradeData m_CrafterAdj = null;
    public bool m_bIsUpgradeActive = false;

    public void Update()
    {
        if (m_bIsUpgradeActive)
            m_iCurrentProduceAmount = m_iBaseProduceAmount + 2;
        else
            m_iCurrentProduceAmount = m_iBaseProduceAmount;

        Update_Produce();
    }
    public override void OnInteract()
    {
        return;
    }

    public override void RecalculateBonus()
    {
        if (!m_CrafterAdj.m_bActivate)
            return;

        bool m_bIsNearIron = false;
        bool m_bIsNearCrystal = false;

        foreach (Vector3Int Near in m_ListNearCell)
        {
            PlacedBuilding building = GridDataManager.Instance.Get_PlacedBuilding(Near);

            if (building != null && building.m_placedBuilding is Producer)
            {
                Producer producer = building.m_placedBuilding as Producer;

                switch(producer.Get_Recipe().m_OutputResources.m_item.m_itemName)
                {
                    case "IronIngot":
                        m_bIsNearIron = true;
                        break;

                    case "Crystal":
                        m_bIsNearCrystal = true;
                        break;

                    default:
                        break;
                }

                if (m_bIsNearIron && m_bIsNearCrystal)
                {
                    m_bIsUpgradeActive = true;
                    return;
                }
            }

        }
    }
}
