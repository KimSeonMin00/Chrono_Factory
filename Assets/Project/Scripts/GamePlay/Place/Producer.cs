using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Producer : Building
{
    [Header("Recipe Data")]
    [SerializeField] private RecipeData m_RecipeData;

    public float m_fTime = 0f;
    public bool m_bIsProduce = false;

    public void Update_Produce()
    {
        if (m_bIsProduce)
        {
            m_fTime += Time.deltaTime;

            ResourceManager.Instance.Add_Heat(m_Data.m_fHeatPerSecond * Time.deltaTime);
            ResourceManager.Instance.Add_Pollution(m_Data.m_fPollutionPerSecond * Time.deltaTime);

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
        return;
    }

    public void Set_Recipe(RecipeData recipe)
    {
        if(recipe != null)
            m_RecipeData = recipe;
    }

    public bool HasResource()
    {
        if (m_RecipeData == null)
            return false;

        List<ResourceAmount> RequireResources = m_RecipeData.m_InputResources;

        foreach (var resource in RequireResources)
        {
            if (ResourceManager.Instance.Get_ResourceAmount(resource.m_item) < resource.m_iAmount)
                return false;
        }

        return true;
    }

    public void Consume_Resource()
    {
        if (m_RecipeData == null)
            return;

        List<ResourceAmount> RequireResources = m_RecipeData.m_InputResources;

        foreach (var resource in RequireResources)
        {
            ResourceManager.Instance.Consume_Resource(resource.m_item, resource.m_iAmount);
        }

        m_bIsProduce = true;
    }

    public void ProduceItem()
    {
        if (m_RecipeData == null)
            return;

        ResourceManager.Instance.Add_Resource(m_RecipeData.m_OutputResources.m_item, m_RecipeData.m_OutputResources.m_iAmount);
        ResourceManager.Instance.Produce_Effect(m_RecipeData.m_OutputResources.m_item, transform.position);

        m_bIsProduce = false;
    }

}
