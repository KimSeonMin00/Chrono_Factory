using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public int m_iItemID;
    public string m_itemName;
    public Sprite m_iconSprite;
    public int m_iValuePerUnit;
}
