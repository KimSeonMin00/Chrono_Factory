using UnityEngine;

public class StorageBuilding : Building
{
    public override void OnInteract()
    {
        UIManager.Instance.OpenUI();
    }

    public override void RecalculateBonus()
    {
        return;
    }
}
