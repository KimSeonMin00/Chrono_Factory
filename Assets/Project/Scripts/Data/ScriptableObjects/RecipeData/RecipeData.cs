using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;



[CreateAssetMenu(fileName = "New RecipeData", menuName = "Scriptable Objects/RecipeData")]
public class RecipeData : ScriptableObject
{
    public string m_recipeName;
    public Sprite m_outputSprite;
    public List<ResourceAmount> m_inputResources;
    public ResourceAmount m_outputResources;
    public float m_fProductionTime;
}

[System.Serializable]
public struct ResourceAmount
{
    public ItemData m_itemData;
    public int m_iAmount;
}
