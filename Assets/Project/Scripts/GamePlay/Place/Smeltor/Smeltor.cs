using Mono.Cecil;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Smeltor : Building
{
    [SerializeField] private RecipeData m_RecipeData;

    public float m_fTime = 0f;
    public bool m_bIsProduce = false;
    public void Update()
    {
        if (m_bIsProduce)
        {
            m_fTime += Time.deltaTime;
            if (m_fTime >= m_RecipeData.m_fProductionTime)
            {
                ProduceItem();
                m_fTime = 0f;
            }
        }
        else
        {
            if (HasResource())
                Consume_Resource();
            else
                return;
        }
    }
    public override void OnInteract()
    {
        return;
    }

    public override void RecalculateBonus()
    {
        
    }

    public bool HasResource()
    {
        List<ResourceAmount> RequireResources = m_RecipeData.m_InputResources;

        foreach(var resource in RequireResources)
        {
            if (ResourceManager.Instance.Get_ResourceAmount(resource.m_item) < resource.m_iAmount)
                return false;
        }

        return true;
    }

    public void Consume_Resource()
    {
        List<ResourceAmount> RequireResources = m_RecipeData.m_InputResources;

        foreach (var resource in RequireResources)
        {
            ResourceManager.Instance.Consume_Resource(resource.m_item, resource.m_iAmount);
        }

        m_bIsProduce = true;
    }

    public void ProduceItem()
    {       
        ResourceManager.Instance.Add_Resource(m_RecipeData.m_OutputResources.m_item, m_RecipeData.m_OutputResources.m_iAmount);
        m_bIsProduce = false;
    }
}
