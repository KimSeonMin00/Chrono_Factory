using UnityEngine;
using System.Collections.Generic;
using System;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<ResourceType, int> m_Resources = new Dictionary<ResourceType, int>();

    public event Action<ResourceType, int> OnResourceChanged;
    protected override void Awake()
    {
        base.Awake();

        foreach(ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            m_Resources[type] = 0;
        }
    }

    public void Add_Resource(ResourceType type, int iAmount)
    {
        m_Resources[type] += iAmount;
        OnResourceChanged?.Invoke(type, m_Resources[type]);

        Debug.Log($"{type} : {m_Resources[type]}");
    }

    public bool Consume_Resource(ResourceType type, int iAmount)
    {
        if (m_Resources[type] >= iAmount)
        {
            m_Resources[type] += iAmount;
            OnResourceChanged?.Invoke(type, m_Resources[type]);
            return true;
        }

        return false;   
    }
}
