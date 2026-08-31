using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NumberSprite", menuName = "Scriptable Objects/NumberSprite")]
public class NumberSprite : ScriptableObject
{
    public List<Number> m_numberList;
}

[System.Serializable]
public struct Number
{
    public int m_iNumber;
    public Sprite m_sprite;
}