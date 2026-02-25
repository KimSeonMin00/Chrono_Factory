using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

[System.Serializable]
public class ColorElement
{
    public Image image;
    public TextMeshProUGUI textmesh;
    public Light light;

    public void ApplyColor(Color c)
    {
        if (image != null) image.color = c;
        if (textmesh != null) textmesh.color = c;
        if (light != null) light.color = c;
    }
}
public class Change_Color : MonoBehaviour
{
    private bool m_bActivate;
    [Header("Reference")]
    [SerializeField] private ColorElement m_ColorRef;
    [SerializeField] private Color m_ColorOrigin;
    [SerializeField] private Color m_ColorChange;
    public float m_fChangeDelay;
    public float m_fChangeTime;
    private float m_fTime;
    public bool m_bChanged;

    void Start()
    {
    }
    void Update()
    {
        if (!m_bActivate)
            return;

        if(!m_bChanged)
            m_fTime += Time.deltaTime;

        if(m_fTime >= m_fChangeDelay)
        {
            StartCoroutine(StartChangeColor(m_fChangeTime));

            m_fTime = 0;
        }
    }

    public void Activate(bool bActivate)
    {
        if (bActivate == m_bActivate)
            return;

        if (m_bActivate)
        {
            m_ColorRef.ApplyColor(m_ColorOrigin);
            StopAllCoroutines();
        }

        m_bChanged = false;
        m_bActivate = bActivate;
        m_fTime = 0;
    }

    private IEnumerator StartChangeColor(float fChangedTime)
    {
        m_bChanged = true;

        m_ColorRef.ApplyColor(m_ColorChange);

        yield return new WaitForSeconds(fChangedTime);

        m_ColorRef.ApplyColor(m_ColorOrigin);

        m_bChanged = false;

        yield return null;
    }
}
