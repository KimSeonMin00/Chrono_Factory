using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

//업그레이드 포인트와 업그레이트 Data, Effect 관리
public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private PermanantData m_playerData;//업그레이드 포인트
    [SerializeField] private List<UpgradeData> m_upgradeList;//Upgrade Data
    private UpgradeEffectRegistry m_upgradeEffects;//실제 Upgrade Effect 모음

    public event Action<int> OnPointChanged;

    private Coroutine m_calculateCourutine;

    protected override void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            Reset_Upgrade();

            m_upgradeEffects = new UpgradeEffectRegistry();
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
        m_playerData.m_iTotalPoint += iPoint;
    }

    public int Get_Point()
    {
        return m_playerData.m_iTotalPoint;
    }

    public void Start_CalculatePoint()
    {
        m_calculateCourutine = StartCoroutine(Calculate_Point());
    }

    //게임 오버 후 포인트를 정산 하는 코루틴, 자원이 서서히 줄어듬에 따라 포인트가 점점 올라가는 것을 시각적으로 보여주기 위함
    IEnumerator Calculate_Point()
    {
        var m_itemList = ResourceManager.Instance.m_itemDataList;
        int iCurrentPoints = m_playerData.m_iTotalPoint;

        yield return new WaitForSeconds(2f);

        foreach (var item in m_itemList)
        {
            if (item.m_iValuePerUnit == 0)
                continue;

            while (ResourceManager.Instance.Consume_Resource(item, 1))
            {               
                m_playerData.m_iTotalPoint += item.m_iValuePerUnit;
                OnPointChanged?.Invoke(m_playerData.m_iTotalPoint);

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
            if (iCost <= m_playerData.m_iTotalPoint)
            {
                m_playerData.m_iTotalPoint -= iCost;
                OnPointChanged?.Invoke(m_playerData.m_iTotalPoint);
                data.Upgrade_Level();
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    //Save 파일 Load시에만 사용, 업그레이드 조건을 무시하고 활성화
    public void Activate_Upgrade(UpgradeData data)
    {
        if (data != null)
            data.Upgrade_Level();
    }

    public List<UpgradeData> Get_All_Upgrades()
    {
        return m_upgradeList;
    }

    //result 씬에서 화면 클릭시 코루틴을 종료하고 바로 최종 포인트를 계산
    public void SkipCalculate()
    {
        if (GameManager.Instance.m_currentState != GameState.GameOver)
            return;

        if (m_calculateCourutine != null)
            StopCoroutine(m_calculateCourutine);
        else
            return;

            var m_itemList = ResourceManager.Instance.m_itemDataList;
        int iCurrentPoints = m_playerData.m_iTotalPoint;

        foreach (var item in m_itemList)
        {
            int iAmount = ResourceManager.Instance.Get_ResourceAmount(item);
            ResourceManager.Instance.Consume_Resource(item, iAmount);
            m_playerData.m_iTotalPoint += item.m_iValuePerUnit * iAmount;
        }

        OnPointChanged?.Invoke(m_playerData.m_iTotalPoint);
    }

    public void Reset_Upgrade()
    {
        m_playerData.m_iTotalPoint = 0;

        foreach (UpgradeData data in m_upgradeList)
        {
            data.Reset_Level();
        }
    }

    public void Upgrade_Apply(UpgradeEffectType effectType, Building building)
    {
        m_upgradeEffects.Apply(effectType, building);
    }
}
