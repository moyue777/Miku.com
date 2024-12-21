using System.Collections.Generic;
using UnityEngine;

public partial class SuperController
{
    //加载
    public void LoadPlayerData(int currentSavePos)
    {
        Instance.playerData = StaticSaveManager.LoadPlayerData(currentSavePos);
        if (Instance.playerData == null)
        {
            Instance.playerData = new PlayerData();
        }
    }
    //保存
    public void SavePlayerData(int currentSavePos)
    {
        StaticSaveManager.SavePlayerData(Instance.playerData, currentSavePos);
    }

    public List<bool> QueryData()
    {
        List<bool> query_result = new List<bool>(3);
        for (int i = 1; i <= 3; i++)
        {
            if (StaticSaveManager.IsSaveSlotAvailable(i))
            {
                query_result[i] = true;
            }
        }
        return query_result;
    }
}