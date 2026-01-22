using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockManager : Singleton<UnlockManager>
{
    [SerializeField] private PermanantData m_Data;
    [SerializeField] private List<UpgradeData> m_UpgradeList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public event Action<int> OnPointChanged;

    protected override void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            m_Data.m_iTotalPoint = 0;

            foreach(UpgradeData data in m_UpgradeList)
            {
                data.Reset_Level();
            }
        }

        else
        {
            Destroy(gameObject);
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
        StartCoroutine(Calculate_Point());
    }
    IEnumerator Calculate_Point()
    {
        var m_itemList = ResourceManager.Instance.m_ItemDataList;
        int iCurrentPoints = m_Data.m_iTotalPoint;

        yield return new WaitForSeconds(2f);

        foreach (var item in m_itemList)
        {
            while (ResourceManager.Instance.Consume_Resource(item, 1))
            {
                m_Data.m_iTotalPoint += item.m_iValuePerUnit;
                OnPointChanged?.Invoke(m_Data.m_iTotalPoint);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public bool Try_Upgrade(UpgradeData data)
    {
        if (data != null)
        {
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
}
