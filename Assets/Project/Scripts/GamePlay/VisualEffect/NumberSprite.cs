using UnityEngine;
using System.Collections.Generic;

//tmp를 이용해 숫자이펙트를 구현할시 가시성이 떨어지는 문제가 발생, 해결을 위해 숫자별로 만든 Sprite를 사용
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