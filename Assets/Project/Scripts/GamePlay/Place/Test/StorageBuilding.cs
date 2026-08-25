using UnityEngine;

public class StorageBuilding : MonoBehaviour
{
    [SerializeField] private ItemData m_itemdata = null;

    public float m_fProduceCooldown = 3f;
    public float m_fTime = 0f;

    void Update()
    {
        m_fTime += Time.deltaTime;

        if (m_fTime >= m_fProduceCooldown)
        {
            ResourceManager.Instance.Add_Resource(m_itemdata, 1);

            ResourceManager.Instance.Produce_Effect(m_itemdata, transform.position, 1);

            m_fTime = 0;
        }
    }
}
