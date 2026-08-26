using UnityEngine;

public abstract class UpgradeEffect : ScriptableObject
{
    public abstract void Apply(Building building);
}
