using UnityEngine;

//건물 선택시 반투명 상태로 설치 예정 위치에 생성, 설치 불가시 붉은색으로 변경
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
