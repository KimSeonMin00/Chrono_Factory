using UnityEngine;

public class VentUpgradeEffect : IUpgradeEffect
{
    public void Apply(Building building)
    {
        ResourceManager.Instance.Add_Resource(ItemDatabase.Instance.Get_ItemData(1), 1);
        ResourceManager.Instance.Produce_Effect(ItemDatabase.Instance.Get_ItemData(1), building.transform.position, 1);
    }
}
