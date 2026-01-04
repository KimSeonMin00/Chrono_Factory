using UnityEngine;

public class Extractor : Building
{
    [SerializeField]private ResourceType m_Resourcetype = ResourceType.None;
    public override void OnInteract()
    {
        return;
    }

    public void SetUp_Resource(ResourceType type)
    {
        m_Resourcetype = type;
    }
}
