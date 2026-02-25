using UnityEngine;

public class GhostObject : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private Color m_EnableColor;
    [SerializeField] private Color m_DisableColor;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        PlacementController.Instance.Set_GhostObject(this);
    }

    public void Set_Ghost(BuildingData data)
    {
        m_SpriteRenderer.sprite = data.m_IconSprite;
    }

    public void Update_Ghost(Vector3 vecPos, bool bEnablePlace)
    {
        transform.position = vecPos;

        if (bEnablePlace)
            m_SpriteRenderer.color = m_EnableColor;

        else
            m_SpriteRenderer.color = m_DisableColor;
    }
}
