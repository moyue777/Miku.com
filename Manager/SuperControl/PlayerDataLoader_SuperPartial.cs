using System.Collections.Generic;
using System;
/// <summary>
/// 仅管理玩家的数据，调用静态加载工具
/// </summary>
public partial class SuperController
{
    /// <summary>
    /// 设置玩家数据
    /// </summary>
    public void SetPlayerData(PlayerData playerData)
    {
        Instance.playerData = playerData;
    }
    /// <summary>
    /// 加载指定栏位的数据
    /// </summary>
    /// <param name="currentSavePos"></param>
    public void LoadPlayerData(int currentSavePos)
    {
        Instance.playerData = StaticSaveManager.LoadPlayerData(currentSavePos);
        if (Instance.playerData == null)
        {
            Instance.playerData = new PlayerData();
        }
        plotManager.UpdateList();
    }
    
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="currentSavePos"></param>
    public void SavePlayerData(int currentSavePos)
    {
        StaticSaveManager.SavePlayerData(Instance.playerData, currentSavePos);
    }

    public Dictionary<string, DateTime> QueryData()
    {
        return StaticSaveManager.GetFile_Times();
    }
}