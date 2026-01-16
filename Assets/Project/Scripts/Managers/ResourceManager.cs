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

    public event Action<ItemData, int> OnResourceChanged;
    protected override void Awake()
    {
        base.Awake();

        Reset_Resource();
    }

    public void Add_Resource(ItemData data, int iAmount)
    {
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

    public void Reset_Resource()
    {
        foreach (var item in m_ItemDataList)
            if (item != null) m_Resources[item] = 0;
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
            Reset_Resource();
    }
}
