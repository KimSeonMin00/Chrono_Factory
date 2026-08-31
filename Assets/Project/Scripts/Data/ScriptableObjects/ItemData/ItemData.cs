using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public int m_iItemID;
    public string m_itemName;
    public Sprite m_iconSprite;
    public int m_iValuePerUnit;//게임 오버 후 result에서 포인트 정산 때 사용
}
