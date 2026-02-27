using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class ResourceManager : Singleton<ResourceManager>
{
    [Header("Item List")]
    public List<ItemData> m_ItemDataList;
    private Dictionary<ItemData, int> m_Resources = new Dictionary<ItemData, int>();

    [Header("Base Resource")]
    [SerializeField] private ResourceAmount m_StartingResource;

    public Color m_FadeColor;

    public float m_fHeat = 0f;
    public float m_fPollution = 0f;

    public float m_fMaxHeat = 100f;
    public float m_fMaxPollution = 100f;

    public event Action<ItemData, int> OnResourceChanged;

    bool m_bStop = false;
    protected override void Awake()
    {
        base.Awake();

        Reset_Resource();
    }

    public void Add_Resource(ItemData data, int iAmount)
    {
        if (m_bStop)
            return;

        m_Resources[data] += iAmount;
        OnResourceChanged?.Invoke(data, m_Resources[data]);
    }

    public bool Consume_Resource(ItemData data, int iAmount)
    {
        if (m_Resources[data] >= iAmount)
        {
            m_Resources[data] -= iAmount;
            OnResourceChanged?.Invoke(data, m_Resources[data]);
            return true;
        }

        return false;   
    }

    public void Add_Heat(float fHeat)
    {
        if (m_bStop)
            return;

        m_fHeat += fHeat;

        if (m_fHeat >= m_fMaxHeat)
        {
            m_fHeat = m_fMaxHeat;
            Fade.Instance.FadeTo("Result", GameState.GameOver, m_FadeColor);
            m_fHeat = 0f;
            m_bStop = true;
        }
    }

    public void Consume_Heat(float fHeat)
    {
        if (m_bStop)
            return;

        m_fHeat -= fHeat;

        if (m_fHeat <= 0f)
            m_fHeat = 0f;
    }

    public float Get_HeatRatio()
    {
        return m_fHeat / m_fMaxHeat;
    }

    public void Add_Pollution(float fPollution)
    {
        if (m_bStop)
            return;

        m_fPollution += fPollution;
        if (m_fPollution >= m_fMaxPollution)
        {
            m_fPollution = m_fMaxPollution;
            Fade.Instance.FadeTo("Result", GameState.GameOver, m_FadeColor);
            m_fPollution = 0f;
            m_bStop = true;
        }
    }

    public void Consume_Pollution(float fPollution)
    {
        if (m_bStop)
            return;

        m_fPollution -= fPollution;

        if (m_fPollution <= 0f)
            m_fPollution = 0f;
    }

    public float Get_PollutionRatio()
    {
        return m_fPollution / m_fMaxPollution;
    }

    public void Reset_Resource()
    {
        m_bStop = false;
        m_fHeat = 0f;
        m_fPollution = 0f;

        foreach (var item in m_ItemDataList)
            if (item != null) m_Resources[item] = 0;

        if(m_StartingResource.m_item != null)
            Add_Resource(m_StartingResource.m_item, m_StartingResource.m_iAmount);        
    }

    public int Get_ResourceAmount(ItemData type)
    {
        return m_Resources[type];
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Result")
            UnlockManager.Instance.Start_CalculatePoint();
    }

    public void Produce_Effect(ItemData data, Vector3 vecPos, int iAmount)
    {
        GameObject go = PoolManager.Instance.Create_Pool();

        go.transform.position = vecPos + new Vector3(0f, 1.5f, 0f);
        go.GetComponent<ProdueSpriteEffect>().Init(data, iAmount);
    }
}
