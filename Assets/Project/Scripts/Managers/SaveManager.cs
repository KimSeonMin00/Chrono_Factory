using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    string m_savePath =>
        Path.Combine(
        Directory.GetParent(Application.dataPath).FullName,
        "SaveData",
        "save.json");
    
    public void Save()
    {
        SaveData saveData = new SaveData();

        saveData.m_placedBuildings = new List<PlacedBuildingSaveData>();

        foreach(var placedBuilding in GridDataManager.Instance.Get_All_PlacedBuilding())
        {
            PlacedBuildingSaveData data = new PlacedBuildingSaveData();

            Vector3Int vecCellPos = placedBuilding.Key;
            int iBuildingID = placedBuilding.Value.m_data.m_iBuildingID;

            data.m_iBuildingID = iBuildingID;
            data.m_iX = vecCellPos.x;
            data.m_iY = vecCellPos.y;

            saveData.m_placedBuildings.Add(data);
        }

        saveData.m_playerData = new PlayerSaveData();

        saveData.m_playerData.m_iTotalPoint = UpgradeManager.Instance.Get_Point();




        string json = 
            JsonUtility.ToJson(saveData, true);

        File.WriteAllText(m_savePath, json);
    }
}
