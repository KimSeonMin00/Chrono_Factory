using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Producer : Building
{
    [Header("Recipe Data")]
    [SerializeField] protected RecipeData m_RecipeData;

    public event Action OnProduced; 

    public float m_fTime = 0f;
    public bool m_bIsProduce = false;

    public float m_fBaseProduceSpeed = 1.0f;
    public int m_iBaseProduceAmount = 1;

    public float m_fCurrentProduceSpeed = 1.0f;
    public int m_iCurrentProduceAmount = 1;

    public bool m_bHasted = false;
    public float m_fHastedTime;
    public void Update_Produce()
    {
        if (m_bIsProduce)
        {
            m_fTime += Time.deltaTime * m_fCurrentProduceSpeed;

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

    public RecipeData Get_Recipe()
    {
        return m_RecipeData;
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

        ResourceManager.Instance.Add_Resource(m_RecipeData.m_OutputResources.m_item, m_RecipeData.m_OutputResources.m_iAmount * m_iCurrentProduceAmount);
        ResourceManager.Instance.Produce_Effect(m_RecipeData.m_OutputResources.m_item, transform.position, m_RecipeData.m_OutputResources.m_iAmount * m_iCurrentProduceAmount);

        OnProduced?.Invoke();

        m_bIsProduce = false;
    }

    public void Haste()
    {
        m_fHastedTime = 2f;

        if (!m_bHasted)
        {
            m_bHasted = true;
            m_fCurrentProduceSpeed = 5f;
            StartCoroutine(HasteBuilding());
        }
    }

    private IEnumerator HasteBuilding()
    {
        while (true)
        {
            m_fHastedTime -= Time.deltaTime;

            if (m_fHastedTime <= 0f)
            {
                m_fCurrentProduceSpeed = m_fBaseProduceSpeed;
                m_bHasted = false;

                yield break;
            }

            yield return null;
        }
    }
}
