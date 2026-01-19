using Mono.Cecil;
using UnityEngine;

public class Smeltor : Building
{
    [SerializeField] private ItemData m_input;
    [SerializeField] private ItemData m_output;

    public float m_fProduceCooldown = 2f;
    public float m_fTime = 0f;
    public void Update()
    {
        m_fTime += Time.deltaTime;
        if (m_fTime >= m_fProduceCooldown)
        {
            if (ResourceManager.Instance.Consume_Resource(m_input, 2))
            {
                ResourceManager.Instance.Add_Resource(m_output, 1);
                m_fTime = 0;
            }
        }
    }
    public override void OnInteract()
    {
        return;
    }

    public override void RecalculateBonus()
    {
        
    }
}
