using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;



[CreateAssetMenu(fileName = "New RecipeData", menuName = "Scriptable Objects/RecipeData")]
public class RecipeData : ScriptableObject
{
    public string m_recipeName;
    public Sprite m_OutputSprite;
    public List<ResourceAmount> m_InputResources;
    public ResourceAmount m_OutputResources;
    public float m_fProductionTime;
}

[System.Serializable]
public struct ResourceAmount
{
    public ItemData m_item;
    public int m_iAmount;
}
