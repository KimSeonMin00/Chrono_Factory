using NUnit.Framework;
using TMPro;
using UnityEngine;


public class ProdueSpriteEffect : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private SpriteRenderer m_numberSpriteRenderer;

    public NumberSprite m_numSprite;
    public float m_fTime;
    public float m_fDisableTime = 0.2f;

    private Color m_color;
    private Color m_textColor;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        m_fTime += Time.deltaTime;

        transform.Translate(transform.up * 2f * Time.deltaTime);
        m_color.a -= 2f * Time.deltaTime;
        m_textColor.a -= 2f * Time.deltaTime;
        if (m_color.a <= 0f)
        {
            m_color.a = 0f;
            m_textColor.a = 0f;
        }
        m_spriteRenderer.color = m_color;
        m_numberSpriteRenderer.color = m_textColor;

        if (m_fTime >= m_fDisableTime)
        {
            m_color.a = 1f;
            m_spriteRenderer.color = m_color;

            m_textColor.a = 1f;
            m_numberSpriteRenderer.color = m_textColor;
            gameObject.SetActive(false);
        }
    }

    public void Init(ItemData data, int iAmount)
    {
        m_spriteRenderer.sprite = data.m_iconSprite;

        m_fTime = 0f;

        m_color = m_spriteRenderer.color;
        m_numberSpriteRenderer.gameObject.SetActive(true);

        if (iAmount > 1)
        {

            foreach (var num in m_numSprite.m_numberList)
            {
                if (num.m_iNumber == iAmount)
                {
                    m_numberSpriteRenderer.sprite = num.m_sprite;
                    break;
                }
            }
        }
        else
            m_numberSpriteRenderer.gameObject.SetActive(false);

        m_textColor = m_numberSpriteRenderer.color;
    }
}
