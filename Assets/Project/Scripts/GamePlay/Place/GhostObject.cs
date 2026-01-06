using UnityEditor.Build;
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

    public void Set_Ghost(GameObject goPrefab)
    {
        m_SpriteRenderer.sprite = goPrefab.GetComponent<SpriteRenderer>().sprite;
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
