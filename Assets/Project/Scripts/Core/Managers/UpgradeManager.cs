using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private PermanantData m_Data;
    [SerializeField] private List<UpgradeData> m_UpgradeList;
    private UpgradeEffectRegistry m_UpgradeEffects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public event Action<int> OnPointChanged;

    private Coroutine m_Calculate;

    protected override void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            Reset_Upgrade();

            m_UpgradeEffects = new UpgradeEffectRegistry();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InputManager.Instance.OnLeftClicked += SkipCalculate;
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnLeftClicked -= SkipCalculate;
        }      
    }
    public void Add_Point(int iPoint)
    {
        m_Data.m_iTotalPoint += iPoint;
    }

    public int Get_Point()
    {
        return m_Data.m_iTotalPoint;
    }

    public void Start_CalculatePoint()
    {
        m_Calculate = StartCoroutine(Calculate_Point());
    }
    IEnumerator Calculate_Point()
    {
        var m_itemList = ResourceManager.Instance.m_ItemDataList;
        int iCurrentPoints = m_Data.m_iTotalPoint;

        yield return new WaitForSeconds(2f);

        foreach (var item in m_itemList)
        {
            if (item.m_iValuePerUnit == 0)
                continue;

            while (ResourceManager.Instance.Consume_Resource(item, 1))
            {               
                m_Data.m_iTotalPoint += item.m_iValuePerUnit;
                OnPointChanged?.Invoke(m_Data.m_iTotalPoint);

                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    public bool Try_Upgrade(UpgradeData data)
    {
        if (data != null)
        {
            if (data.m_bActivate)
                return false;

            int iCost = data.Get_Cost();
            if (iCost <= m_Data.m_iTotalPoint)
            {
                m_Data.m_iTotalPoint -= iCost;
                OnPointChanged?.Invoke(m_Data.m_iTotalPoint);
                data.Upgrade_Level();
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public void Activate_Upgrade(UpgradeData data)
    {
        if (data != null)
            data.Upgrade_Level();
    }

    public List<UpgradeData> Get_All_Upgrades()
    {
        return m_UpgradeList;
    }

    public void SkipCalculate()
    {
        if (GameManager.Instance.m_currentState != GameState.GameOver)
            return;

        if (m_Calculate != null)
            StopCoroutine(m_Calculate);
        else
            return;

            var m_itemList = ResourceManager.Instance.m_ItemDataList;
        int iCurrentPoints = m_Data.m_iTotalPoint;

        foreach (var item in m_itemList)
        {
            int iAmount = ResourceManager.Instance.Get_ResourceAmount(item);
            ResourceManager.Instance.Consume_Resource(item, iAmount);
            m_Data.m_iTotalPoint += item.m_iValuePerUnit * iAmount;
        }

        OnPointChanged?.Invoke(m_Data.m_iTotalPoint);
    }

    public void Reset_Upgrade()
    {
        m_Data.m_iTotalPoint = 0;

        foreach (UpgradeData data in m_UpgradeList)
        {
            data.Reset_Level();
        }
    }

    public void Upgrade_Apply(UpgradeEffectType effectType, Building building)
    {
        m_UpgradeEffects.Apply(effectType, building);
    }
}
