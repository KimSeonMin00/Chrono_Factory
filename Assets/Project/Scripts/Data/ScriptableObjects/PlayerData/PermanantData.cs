using UnityEngine;

[CreateAssetMenu(fileName = "PermanantData", menuName = "Scriptable Objects/PlayerData/PermanantData")]
public class PermanantData : ScriptableObject
{
    public int m_iTotalPoint;//업그레이드 시 소비되는 포인트
}
