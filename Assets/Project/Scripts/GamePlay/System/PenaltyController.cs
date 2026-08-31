using UnityEngine;

public class PenaltyController : Building
{
    [Header("Consume Penalty")]
    public float m_fHeatConsume;
    public float m_fPollutuionConsume;

    [Header("Building Setting")]
    public float m_fCooldown = 1f;
    public float m_fTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Update()
    {
        m_fTime += Time.deltaTime;

        if (m_fTime >= m_fCooldown)
        {
            ResourceManager.Instance.Consume_Heat(m_fHeatConsume);
            ResourceManager.Instance.Consume_Pollution(m_fPollutuionConsume);

            m_fTime = 0;
        }
    }

    public void Setup(float fHeat, float fPollution)
    {
        m_fHeatConsume = fHeat;
        m_fPollutuionConsume = fPollution;
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
