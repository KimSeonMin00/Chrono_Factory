using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NumberSprite", menuName = "Scriptable Objects/NumberSprite")]
public class NumberSprite : ScriptableObject
{
    public List<Number> m_NumList;
}

[System.Serializable]
public struct Number
{
    public int m_iNum;
    public Sprite m_Sprite;
}