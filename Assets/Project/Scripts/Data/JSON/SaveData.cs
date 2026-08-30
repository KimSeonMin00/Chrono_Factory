using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public PlayerSaveData m_playerData;
    public List<UpgradeSaveData> m_upgrades;
}
