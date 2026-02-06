using UnityEngine;
public class ProdueSpriteEffect : MonoBehaviour
{
    [Header("Components")]
    private SpriteRenderer m_SpriteRenderer;

    public float m_fTime;
    public float m_fDisableTime = 1f;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        m_fTime += Time.deltaTime;

        transform.Translate(transform.up * 1f * Time.deltaTime);

        if (m_fTime >= m_fDisableTime)
            Destroy(this.gameObject);
    }

    public void Init(ItemData data)
    {
        m_SpriteRenderer.sprite = data.m_iconSprite;

        m_fTime = 0f;
    }
}
