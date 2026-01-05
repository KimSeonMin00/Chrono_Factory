using UnityEngine;

public class Extractor : Building
{
    [SerializeField]private ResourceType m_Resourcetype = ResourceType.None;

    public float m_fProduceCooldown = 1f;
    public float m_fTime = 0f;
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
}
