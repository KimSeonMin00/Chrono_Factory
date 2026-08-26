using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<PlacedBuildingSaveData> m_placedBuildings;
    public PlayerSaveData m_playerData;
}
