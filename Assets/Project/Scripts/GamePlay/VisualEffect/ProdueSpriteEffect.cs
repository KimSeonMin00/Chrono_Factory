using NUnit.Framework;
using TMPro;
using UnityEngine;


public class ProdueSpriteEffect : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private SpriteRenderer m_Number;

    public NumberSprite m_NumSprite;
    public float m_fTime;
    public float m_fDisableTime = 0.2f;

    private Color m_Color;
    private Color m_TextColor;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        m_fTime += Time.deltaTime;

        transform.Translate(transform.up * 2f * Time.deltaTime);
        m_Color.a -= 2f * Time.deltaTime;
        m_TextColor.a -= 2f * Time.deltaTime;
        if (m_Color.a <= 0f)
        {
            m_Color.a = 0f;
            m_TextColor.a = 0f;
        }
        m_SpriteRenderer.color = m_Color;
        m_Number.color = m_TextColor;

        if (m_fTime >= m_fDisableTime)
        {
            m_Color.a = 1f;
            m_SpriteRenderer.color = m_Color;

            m_TextColor.a = 1f;
            m_Number.color = m_TextColor;
            gameObject.SetActive(false);
        }
    }

    public void Init(ItemData data, int iAmount)
    {
        m_SpriteRenderer.sprite = data.m_iconSprite;

        m_fTime = 0f;

        m_Color = m_SpriteRenderer.color;
        m_Number.gameObject.SetActive(true);

        if (iAmount > 1)
        {

            foreach (var num in m_NumSprite.m_NumList)
            {
                if (num.m_iNum == iAmount)
                {
                    m_Number.sprite = num.m_Sprite;
                    break;
                }
            }
        }
        else
            m_Number.gameObject.SetActive(false);

        m_TextColor = m_Number.color;
    }
}
