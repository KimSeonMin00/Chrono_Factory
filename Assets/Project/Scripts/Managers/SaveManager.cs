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

        saveData.m_playerData = new PlayerSaveData();

        saveData.m_playerData.m_iTotalPoint = UpgradeManager.Instance.Get_Point();

        saveData.m_upgrades = new List<UpgradeSaveData>();

        foreach(var upgrade in UpgradeManager.Instance.Get_All_Upgrades())
        {
            UpgradeSaveData data = new UpgradeSaveData();

            int iID = data.m_iUpgradeID;
            bool bActivate = data.m_bActivate;

            data.m_iUpgradeID = iID; 
            data.m_bActivate = bActivate;

            saveData.m_upgrades.Add(data);
        }

        string json = 
            JsonUtility.ToJson(saveData, true);

        File.WriteAllText(m_savePath, json);
    }

    public void Load() 
    {
        if (!File.Exists(m_savePath))
        {
            Debug.Log("No SaveFile");
            return;
        }

        string json = File.ReadAllText(m_savePath);

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        PlayerSaveData playerSaveData = saveData.m_playerData;

        UpgradeManager.Instance.Add_Point(playerSaveData.m_iTotalPoint);

        foreach(var data in saveData.m_upgrades)
        {
            UpgradeData upgradeData = UpgradeDatabase.Instance.Get_UpgradeData(data.m_iUpgradeID);

            if (data.m_bActivate)
                UpgradeManager.Instance.Activate_Upgrade(upgradeData);
        }

       
    }
}
