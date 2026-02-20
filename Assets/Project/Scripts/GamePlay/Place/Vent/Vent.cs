using UnityEngine;

public class Vent : PenaltyController
{
    [Header("Upgrade Data")]
    [SerializeField] private UpgradeData m_VentUpgrade = null;
    [SerializeField] private ItemData m_UpgradeResource;
    

    protected override void Update()
    {
        m_fTime += Time.deltaTime;

        if (m_fTime >= m_fCooldown)
        {
            ResourceManager.Instance.Consume_Heat(m_fHeatConsume);
            ResourceManager.Instance.Consume_Pollution(m_fPollutuionConsume);

            if (m_VentUpgrade.m_bActivate)
            {
                ResourceManager.Instance.Add_Resource(m_UpgradeResource, 1);
                ResourceManager.Instance.Produce_Effect(m_UpgradeResource, transform.position);
            }

            m_fTime = 0;
        }
    }
    public override void OnInteract()
    {
        return;
    }

    public override void RecalculateBonus()
    {
        return;
    }
}
