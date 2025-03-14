using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class StaticSaveManager
{
    public const string SaveFolder = "Saves";
    public const string SaveFileExtension = ".save";
    public const string SaveList = ".savelist";

    /// <summary>
    /// 返回文件保存的路径
    /// </summary>
    /// <param name="cursaveSlot"></param>
    /// <returns></returns>
    private static string GetSavePath(int cursaveSlot)
    {
        return Path.Combine(Application.persistentDataPath, SaveFolder, cursaveSlot + SaveFileExtension);
    }
    
    /// <summary>
    /// 保存当前数据
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="saveSlot"></param>
    public static void SavePlayerData(PlayerData playerData, int saveSlot)
    {
        if (IsSaveSlotAvailable(saveSlot))
        {
            string path = GetSavePath(saveSlot);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(path);
            bf.Serialize(file, playerData);
            file.Close();
        }
        else
        {
            Debug.LogError("Save slot is already in use or save slots are full.");
        }
    }

    /// <summary>
    /// 读取存档
    /// </summary>
    /// <param name="saveSlot"></param>
    /// <returns></returns>
    public static PlayerData LoadPlayerData(int saveSlot)
    {
        string path = GetSavePath(saveSlot);
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            PlayerData playerData = (PlayerData)bf.Deserialize(file);
            file.Close();
            return playerData;
        }
        return null;
    }

    /// <summary>
    /// 删除存档
    /// </summary>
    /// <param name="saveSlot"></param>
    public static void DeleteSave(int saveSlot)
    {
        string path = GetSavePath(saveSlot);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    /// <summary>
    /// 检查存档是否存在
    /// </summary>
    /// <param name="saveSlot"></param>
    /// <returns></returns>
    public static bool IsSaveSlotAvailable(int saveSlot){
        if ( File.Exists(GetSavePath(saveSlot)) )
        {
            return true;
        }else{
            return false;
        }
    }
    
}

