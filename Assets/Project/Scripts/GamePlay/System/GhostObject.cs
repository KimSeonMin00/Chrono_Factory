using UnityEngine;

public class GhostObject : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;

    [SerializeField] private Color m_enableColor;
    [SerializeField] private Color m_disableColor;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        PlacementController.Instance.Set_GhostObject(this);
    }

    public void Set_Ghost(BuildingData data)
    {
        m_spriteRenderer.sprite = data.m_iconSprite;
    }

    public void Update_Ghost(Vector3 vecPos, bool bEnablePlace)
    {
        transform.position = vecPos;

        if (bEnablePlace)
            m_spriteRenderer.color = m_enableColor;

        else
            m_spriteRenderer.color = m_disableColor;
    }
}
