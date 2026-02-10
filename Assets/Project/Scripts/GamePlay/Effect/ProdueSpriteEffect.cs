using NUnit.Framework;
using UnityEngine;


public class ProdueSpriteEffect : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    public float m_fTime;
    public float m_fDisableTime = 0.2f;

    private Color m_Color;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        m_fTime += Time.deltaTime;

        transform.Translate(transform.up * 3f * Time.deltaTime);
        m_Color.a -= 3f * Time.deltaTime;
        if (m_Color.a <= 0f)
            m_Color.a = 0f;
        m_SpriteRenderer.color = m_Color;

        if (m_fTime >= m_fDisableTime)
        {
            m_Color.a = 1f;
            m_SpriteRenderer.color = m_Color;
            gameObject.SetActive(false);
        }
    }

    public void Init(ItemData data)
    {
        m_SpriteRenderer.sprite = data.m_iconSprite;

        m_fTime = 0f;

        m_Color = m_SpriteRenderer.color;
    }
}
