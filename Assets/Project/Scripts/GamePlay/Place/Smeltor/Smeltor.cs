using Mono.Cecil;
using UnityEngine;

public class Smeltor : Building
{
    [SerializeField] private ItemData data;

    public float m_fProduceCooldown = 2f;
    public float m_fTime = 0f;
    public void Update()
    {
        m_fTime += Time.deltaTime;
        if (m_fTime >= m_fProduceCooldown)
        {
            if (ResourceManager.Instance.Consume_Resource(data, 2))
            {
                ResourceManager.Instance.Add_Resource(data, 1);
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
