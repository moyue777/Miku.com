using System.Collections.Generic;
/// <summary>
/// 仅管理玩家的数据，调用静态加载工具
/// </summary>
public partial class SuperController
{
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